import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import AddIcon from "@mui/icons-material/Add";
import { useNavigate } from "react-router";
import useNotifications from "../../context/useNotifications/useNotifications";
import PageContainer from "../../components/PageContainer";
import CustomDataGrid from "../../components/grid/CustomDataGrid";
import { useCallback, useEffect, useState } from "react";
import { GridEventListener } from "@mui/x-data-grid";
import { useLoading } from "../../context/useLoading/useLoading";
import DeleteIcon from "@mui/icons-material/Delete";
import { useModal } from "../../context/useModal/useModal";
import { deleteUsers, getUsers } from "./services/UserService";
import { IUserTable } from "./models/User";
import UserColumns from "./components/grid/UserColumns";

export default function UsersPage() {
  const navigate = useNavigate();
  const notifications = useNotifications();
  const { loading, startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const [users, setUsers] = useState<IUserTable[]>([]);
  const [idsToDelete, setIdsToDelete] = useState<string[]>([]);

  const loadData = useCallback(async () => {
    startLoading();
    getUsers()
      .then((data) => {
        setUsers(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar os utilizadores.");
        stopLoading();
      });
  }, []);

  useEffect(() => {
    loadData();
  }, []);

  const handleRefresh = useCallback(
    () => loadData(),
    [startLoading, stopLoading, loadData]
  );

  const handleRowClick = useCallback<GridEventListener<"rowClick">>(
    ({ row }) => {
      navigate(`/users/${row.id}`);
    },
    [navigate]
  );

  const handleCreateClick = useCallback(() => {
    navigate("/users/new");
  }, [navigate]);

  const handleDeleteClick = useCallback(() => {
    startLoading();
    deleteUsers(idsToDelete)
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show(
            `${
              idsToDelete.length === 1
                ? "Utilizador apagado"
                : "Utilizadores apagados"
            } com sucesso.`,
            {
              severity: "success",
              autoHideDuration: 5000
            }
          );
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar utilizadores"
          );
        }
        handleRefresh();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar utilizadores");
        stopLoading();
      });
  }, [idsToDelete]);

  const handleDeleteModal = useCallback(() => {
    showWarning(
      `Tem a certeza que pretende apagar ${
        idsToDelete.length === 1
          ? "o utilizador selecionado"
          : "os utilizadores selecionados"
      }?`,
      handleDeleteClick
    );
  }, [idsToDelete]);

  return (
    <PageContainer
      title={"Utilizadores"}
      breadcrumbs={[{ title: "Utilizadores" }]}
      actions={
        <Stack direction="row" alignItems="center" spacing={2}>
          {idsToDelete.length > 0 && (
            <Button
              variant="contained"
              color="error"
              onClick={handleDeleteModal}
              startIcon={<DeleteIcon />}
            >
              Apagar
            </Button>
          )}

          <Button
            variant="contained"
            onClick={handleCreateClick}
            startIcon={<AddIcon />}
          >
            Adicionar
          </Button>
        </Stack>
      }
    >
      <CustomDataGrid
        columns={UserColumns()}
        rows={users}
        loading={loading}
        handleRowClick={handleRowClick}
        handleRefresh={handleRefresh}
        setIdsToDelete={setIdsToDelete}
      />
    </PageContainer>
  );
}
