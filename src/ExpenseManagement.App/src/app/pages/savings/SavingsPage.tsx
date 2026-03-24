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
import dayjs from "dayjs";
import { CategoryEnumPt } from "../../models/Category";
import { deleteSavings, getSavings } from "./services/SavingService";
import { ISavingTable } from "./models/Saving";
import SavingColumns from "./components/grid/SavingColumns";

export default function SavingsPage() {
  const navigate = useNavigate();
  const notifications = useNotifications();
  const { loading, startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const [savings, setSavings] = useState<ISavingTable[]>([]);
  const [idsToDelete, setIdsToDelete] = useState<string[]>([]);

  const loadData = useCallback(async () => {
    startLoading();
    getSavings()
      .then((data) => {
        const mapped: ISavingTable[] = data.map((x) => ({
          ...x,
          category: CategoryEnumPt[x.category],
          date: dayjs(x.date).format("DD/MM/YYYY")
        }));
        setSavings(mapped);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar as poupanças.");
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
      navigate(`/savings/${row.id}`);
    },
    [navigate]
  );

  const handleCreateClick = useCallback(() => {
    navigate("/savings/new");
  }, [navigate]);

  const handleDeleteClick = useCallback(() => {
    startLoading();
    deleteSavings(idsToDelete)
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show(
            `${
              idsToDelete.length === 1
                ? "Poupança apagada"
                : "Poupanças apagadas"
            } com sucesso.`,
            {
              severity: "success",
              autoHideDuration: 5000
            }
          );
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar poupanças"
          );
        }
        handleRefresh();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar poupanças");
        stopLoading();
      });
  }, [idsToDelete]);

  const handleDeleteModal = useCallback(() => {
    showWarning(
      `Tem a certeza que pretende apagar ${
        idsToDelete.length === 1
          ? "a poupança selecionada"
          : "as poupanças selecionadas"
      }?`,
      handleDeleteClick
    );
  }, [idsToDelete]);

  return (
    <PageContainer
      title={"Poupanças"}
      breadcrumbs={[{ title: "Poupanças" }]}
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
        columns={SavingColumns()}
        rows={savings}
        loading={loading}
        handleRowClick={handleRowClick}
        handleRefresh={handleRefresh}
        setIdsToDelete={setIdsToDelete}
      />
    </PageContainer>
  );
}
