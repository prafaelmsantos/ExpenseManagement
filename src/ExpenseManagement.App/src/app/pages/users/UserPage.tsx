import { useForm, FormProvider } from "react-hook-form";
import { Button } from "@mui/material";
import { zodResolver } from "@hookform/resolvers/zod";
import PageContainer, { Breadcrumb } from "../../components/PageContainer";
import AddIcon from "@mui/icons-material/Add";
import { useMatch, useNavigate, useParams } from "react-router";
import { useCallback, useEffect, useState } from "react";
import EditIcon from "@mui/icons-material/Edit";
import SendIcon from "@mui/icons-material/Send";
import CloseIcon from "@mui/icons-material/Close";
import { IMode } from "../../models/Mode";
import useNotifications from "../../context/useNotifications/useNotifications";
import { useLoading } from "../../context/useLoading/useLoading";

import DeleteIcon from "@mui/icons-material/Delete";

import { useModal } from "../../context/useModal/useModal";
import { IUserSchema, userSchema } from "./services/UserSchema";
import { IUser } from "./models/User";
import {
  createUser,
  deleteUsers,
  getUser,
  updateUser
} from "./services/UserService";
import UserForm from "./components/form/UserForm";

export default function UserPage() {
  const baseUrl: string = "/users";

  const navigate = useNavigate();
  const params = useParams<{ userId: string }>();
  const userId = params.userId;

  const notifications = useNotifications();
  const { startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const methods = useForm<IUserSchema>({
    resolver: zodResolver(userSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });

  const { reset, handleSubmit } = methods;

  const [user, setUser] = useState<IUser>({
    id: "",
    userName: "",
    firstName: "",
    lastName: "",
    role: { id: "", name: "" }
  });

  const [mode, setMode] = useState<IMode>(IMode.PREVIEW);

  const matchNew = useMatch({ path: "/users/new", end: true });
  const matchEdit = useMatch({
    path: "/users/:userId/edit",
    end: true
  });
  const matchDetail = useMatch({ path: "/users/:userId", end: true });

  useEffect(() => {
    if (matchNew) setMode(IMode.ADD);
    else if (matchEdit) setMode(IMode.EDIT);
    else if (matchDetail) setMode(IMode.PREVIEW);
  }, [matchNew, matchEdit, matchDetail]);

  const loadData = useCallback(async () => {
    if (userId) {
      startLoading();
      getUser(userId)
        .then((data) => {
          setUser(data);
          stopLoading();
        })
        .catch((e: Error) => {
          void handleClose();
          showError(e.message, "Erro ao tentar carregar o utilizador");
          stopLoading();
        });
    } else if (!matchNew) {
      void handleClose();
    }
  }, [userId]);

  useEffect(() => {
    void loadData();
  }, [userId]);

  useEffect(() => {
    void reset(user);
  }, [user]);

  const handleClose = () => navigate(baseUrl);

  const handleSumbitEdit = async (user: IUser) => {
    startLoading();
    updateUser(user)
      .then(() => {
        notifications.show("Utilizador atualizado com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });

        navigate(`/users/${user.id}`);
        void loadData();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar atualizar o utilizador");
        stopLoading();
      });
  };

  const handleSumbitAdd = async (user: IUser) => {
    startLoading();
    createUser(user)
      .then(() => {
        notifications.show("Utilizador criado com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar criar o utilizador");
        stopLoading();
      });
  };

  const handleEdit = () => {
    navigate(`/users/${userId}/edit`);
  };

  const handleRollback = () => {
    navigate(`/users/${userId}`);
    void loadData();
  };

  const handleDeleteClick = () => {
    startLoading();
    deleteUsers([userId ?? ""])
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show("Utilizador apagado com sucesso.", {
            severity: "success",
            autoHideDuration: 5000
          });
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar o utilizador"
          );
        }
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar o utilizador");
        stopLoading();
      });
  };

  const handleDeleteModal = () => {
    showWarning(
      "Tem a certeza que pretende apagar o utilizador selecionado?",
      handleDeleteClick
    );
  };

  const breadcrumbs: Breadcrumb[] = [
    { title: "Utilizadores", path: baseUrl },
    ...(mode === IMode.ADD
      ? [{ title: "Novo" }]
      : mode === IMode.EDIT
        ? [
            { title: user.userName, path: `${baseUrl}/${user.id}` },
            { title: "Editar" }
          ]
        : [{ title: user.userName }])
  ];

  return (
    <PageContainer
      title={
        mode === IMode.ADD
          ? "Criar Utilizador"
          : mode === IMode.EDIT
            ? "Editar Utilizador"
            : user.userName
      }
      breadcrumbs={breadcrumbs}
      actions={
        <>
          {(mode === IMode.EDIT || mode === IMode.PREVIEW) && (
            <Button
              type={mode === IMode.EDIT ? "submit" : undefined}
              variant="contained"
              color={mode === IMode.PREVIEW ? "error" : "primary"}
              onClick={mode === IMode.EDIT ? handleRollback : handleDeleteModal}
              startIcon={mode === IMode.EDIT ? <CloseIcon /> : <DeleteIcon />}
            >
              {mode === IMode.EDIT ? "Fechar" : "Apagar"}
            </Button>
          )}

          <Button
            type="submit"
            variant="contained"
            onClick={handleSubmit(
              mode === IMode.ADD
                ? handleSumbitAdd
                : mode === IMode.PREVIEW
                  ? handleEdit
                  : handleSumbitEdit
            )}
            startIcon={
              mode === IMode.ADD ? (
                <AddIcon />
              ) : mode === IMode.PREVIEW ? (
                <EditIcon />
              ) : (
                <SendIcon />
              )
            }
          >
            {mode === IMode.PREVIEW ? "Editar" : "Submeter"}
          </Button>
        </>
      }
    >
      <FormProvider {...methods}>
        <UserForm mode={mode} />
      </FormProvider>
    </PageContainer>
  );
}
