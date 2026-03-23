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
import { deleteExpenses, getExpenses } from "./services/ExpenseService";
import { IExpenseTable } from "./models/Expense";
import ExpenseColumns from "./components/grid/ExpenseColumns";

export default function ExpensesPage() {
  const navigate = useNavigate();
  const notifications = useNotifications();
  const { loading, startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const [expenses, setExpenses] = useState<IExpenseTable[]>([]);
  const [idsToDelete, setIdsToDelete] = useState<string[]>([]);

  const loadData = useCallback(async () => {
    startLoading();
    getExpenses()
      .then((data) => {
        setExpenses(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar as despesas.");
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
      navigate(`/expenses/${row.id}`);
    },
    [navigate]
  );

  const handleCreateClick = useCallback(() => {
    navigate("/expenses/new");
  }, [navigate]);

  const handleDeleteClick = useCallback(() => {
    startLoading();
    deleteExpenses(idsToDelete)
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show(
            `${
              idsToDelete.length === 1 ? "Despesa apagada" : "Despesas apagadas"
            } com sucesso.`,
            {
              severity: "success",
              autoHideDuration: 5000
            }
          );
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar despesas"
          );
        }
        handleRefresh();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar despesas");
        stopLoading();
      });
  }, [idsToDelete]);

  const handleDeleteModal = useCallback(() => {
    showWarning(
      `Tem a certeza que pretende apagar ${
        idsToDelete.length === 1
          ? "a despesa selecionada"
          : "as despesas selecionadas"
      }?`,
      handleDeleteClick
    );
  }, [idsToDelete]);

  return (
    <PageContainer
      title={"Despesas"}
      breadcrumbs={[{ title: "Despesas" }]}
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
        columns={ExpenseColumns()}
        rows={expenses}
        loading={loading}
        handleRowClick={handleRowClick}
        handleRefresh={handleRefresh}
        setIdsToDelete={setIdsToDelete}
      />
    </PageContainer>
  );
}
