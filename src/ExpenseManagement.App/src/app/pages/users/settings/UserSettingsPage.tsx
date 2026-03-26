import { FormProvider, useForm } from "react-hook-form";
import { useMatch, useNavigate } from "react-router";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import EditIcon from "@mui/icons-material/Edit";
import SendIcon from "@mui/icons-material/Send";
import CloseIcon from "@mui/icons-material/Close";
import { Button } from "@mui/material";
import UserSettingsForm from "./components/form/UserSettingsForm";
import useNotifications from "../../../context/useNotifications/useNotifications";
import { useLoading } from "../../../context/useLoading/useLoading";
import { useModal } from "../../../context/useModal/useModal";
import {
  IUserSettingsSchema,
  userSettingsSchema
} from "../services/UserSchema";
import { IMode } from "../../../models/Mode";
import PageContainer from "../../../components/PageContainer";
import { IUserSettings } from "../models/User";
import { getUserSettings, updateUserSettings } from "../services/UserService";
import useAuth from "../../../context/useAuth/useAuth";

export default function UserSettingsPage() {
  const baseUrl: string = "/settings";

  const navigate = useNavigate();

  const notifications = useNotifications();
  const { startLoading, stopLoading } = useLoading();
  const { showError } = useModal();
  const { setUser } = useAuth();

  const methods = useForm<IUserSettingsSchema>({
    resolver: zodResolver(userSettingsSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });

  const { handleSubmit, reset } = methods;

  const [userSettings, setUserSettings] = useState<IUserSettings>({
    firstName: "",
    lastName: ""
  });

  const [mode, setMode] = useState<IMode>(IMode.PREVIEW);

  const matchEdit = useMatch({
    path: "/settings/edit",
    end: true
  });

  useEffect(() => {
    if (matchEdit) setMode(IMode.EDIT);
    else setMode(IMode.PREVIEW);
  }, [matchEdit]);

  const loadData = async () => {
    startLoading();
    getUserSettings()
      .then((data) => {
        setUser(data);
        setUserSettings(data);
        stopLoading();
      })
      .catch((e: Error) => {
        void handleClose();
        showError(e.message, "Erro ao tentar carregar o utilizador");
        stopLoading();
      });
  };

  useEffect(() => {
    void loadData();
  }, []);

  useEffect(() => {
    void reset(userSettings);
  }, [userSettings]);

  const handleClose = () => {
    navigate(baseUrl);
  };

  const handleRollback = () => {
    navigate(baseUrl);
    void loadData();
  };

  const handleSumbitEdit = async (userSettings: IUserSettings) => {
    startLoading();
    updateUserSettings(userSettings)
      .then(() => {
        notifications.show("Utilizador atualizado com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });
        navigate(baseUrl);
        void loadData();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar atualizar o utilizador");
        stopLoading();
      });
  };

  const handleEdit = () => {
    navigate("/settings/edit");
  };

  return (
    <PageContainer
      title={"Definições"}
      breadcrumbs={[{ title: "Definições" }]}
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
        <UserSettingsForm mode={mode} />
      </FormProvider>
    </PageContainer>
  );
}
