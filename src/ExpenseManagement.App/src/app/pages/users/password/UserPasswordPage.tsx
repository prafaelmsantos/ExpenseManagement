import { FormProvider, useForm } from "react-hook-form";
import PageContainer from "../../../components/PageContainer";
import { useMatch, useNavigate } from "react-router";
import useNotifications from "../../../context/useNotifications/useNotifications";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  IUserPasswordSchema,
  userPasswordSchema
} from "../services/UserSchema";
import { useLoading } from "../../../context/useLoading/useLoading";
import { useModal } from "../../../context/useModal/useModal";
import { useEffect, useState } from "react";
import EditIcon from "@mui/icons-material/Edit";
import SendIcon from "@mui/icons-material/Send";
import CloseIcon from "@mui/icons-material/Close";
import { IMode } from "../../../models/Mode";
import { updateUserPassword } from "../services/UserService";
import { IUserPassword } from "../models/User";
import { Button } from "@mui/material";
import UserPasswordForm from "./components/form/UserPasswordForm";

export default function UserPasswordPage() {
  const baseUrl: string = "/password";

  const navigate = useNavigate();

  const notifications = useNotifications();
  const { startLoading, stopLoading } = useLoading();
  const { showError } = useModal();

  const methods = useForm<IUserPasswordSchema>({
    resolver: zodResolver(userPasswordSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });

  const { handleSubmit, reset } = methods;

  const [mode, setMode] = useState<IMode>(IMode.PREVIEW);

  const matchEdit = useMatch({
    path: "/password/edit",
    end: true
  });

  useEffect(() => {
    if (matchEdit) setMode(IMode.EDIT);
    else setMode(IMode.PREVIEW);
  }, [matchEdit]);

  const handleSumbitEdit = async (userPassword: IUserPassword) => {
    startLoading();
    updateUserPassword(userPassword)
      .then(() => {
        void reset({ currentPassword: "", newPassword: "" });
        notifications.show("Palavra-passe atualizada com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });
        navigate(baseUrl);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar atualizar a palavra-passe do utilizador."
        );
        stopLoading();
      });
  };

  const handleEdit = () => {
    navigate("/password/edit");
  };

  const handleRollback = () => {
    void reset({ currentPassword: "", newPassword: "" });
    navigate(baseUrl);
  };

  return (
    <PageContainer
      title={"Alterar palavra-passe"}
      breadcrumbs={[{ title: "Alterar palavra-passe" }]}
      actions={
        <>
          {mode === IMode.EDIT && (
            <Button
              type={"submit"}
              variant="contained"
              color={"primary"}
              onClick={handleRollback}
              startIcon={<CloseIcon />}
            >
              {"Fechar"}
            </Button>
          )}

          <Button
            type="submit"
            variant="contained"
            onClick={
              mode === IMode.PREVIEW
                ? handleEdit
                : handleSubmit(handleSumbitEdit)
            }
            startIcon={mode === IMode.PREVIEW ? <EditIcon /> : <SendIcon />}
          >
            {mode === IMode.PREVIEW ? "Editar" : "Submeter"}
          </Button>
        </>
      }
    >
      <FormProvider {...methods}>
        <UserPasswordForm mode={mode} />
      </FormProvider>
    </PageContainer>
  );
}
