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
import { IExpense } from "./models/Expense";
import {
  createExpense,
  deleteExpenses,
  getExpense,
  updateExpense
} from "./services/ExpenseService";
import { expenseSchema, IExpenseSchema } from "./services/ExpenseSchema";
import ExpenseForm from "./components/form/ExpenseForm";

export default function ExpensePage() {
  const baseUrl: string = "/expenses";

  const navigate = useNavigate();
  const params = useParams<{ expenseId: string }>();
  const expenseId = params.expenseId;

  const notifications = useNotifications();
  const { startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const methods = useForm<IExpenseSchema>({
    resolver: zodResolver(expenseSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });

  const { reset, handleSubmit } = methods;

  const [expense, setExpense] = useState<IExpense>({
    id: "",
    amount: 0,
    category: "",
    date: new Date()
  });

  const [mode, setMode] = useState<IMode>(IMode.PREVIEW);

  const matchNew = useMatch({ path: "/expenses/new", end: true });
  const matchEdit = useMatch({
    path: "/expenses/:expenseId/edit",
    end: true
  });
  const matchDetail = useMatch({ path: "/expenses/:expenseId", end: true });

  useEffect(() => {
    if (matchNew) setMode(IMode.ADD);
    else if (matchEdit) setMode(IMode.EDIT);
    else if (matchDetail) setMode(IMode.PREVIEW);
  }, [matchNew, matchEdit, matchDetail]);

  const loadData = useCallback(async () => {
    if (expenseId) {
      startLoading();
      getExpense(expenseId)
        .then((data) => {
          setExpense(data);
          stopLoading();
        })
        .catch((e: Error) => {
          void handleClose();
          showError(e.message, "Erro ao tentar carregar a despesa");
          stopLoading();
        });
    } else if (!matchNew) {
      void handleClose();
    }
  }, [expenseId]);

  useEffect(() => {
    void loadData();
  }, [expenseId]);

  useEffect(() => {
    void reset(expense);
  }, [expense]);

  const handleClose = () => navigate(baseUrl);

  const handleSumbitEdit = async (expense: IExpense) => {
    startLoading();
    updateExpense(expense)
      .then(() => {
        notifications.show("Despesa atualizada com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });

        navigate(`/expenses/${expense.id}`);
        void loadData();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar atualizar a despesa");
        stopLoading();
      });
  };

  const handleSumbitAdd = async (expense: IExpense) => {
    startLoading();
    createExpense(expense)
      .then(() => {
        notifications.show("Despesa criada com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar criar a despesa");
        stopLoading();
      });
  };

  const handleEdit = () => {
    navigate(`/expenses/${expenseId}/edit`);
  };

  const handleRollback = () => {
    navigate(`/expenses/${expenseId}`);
    void loadData();
  };

  const handleDeleteClick = () => {
    startLoading();
    deleteExpenses([expenseId ?? ""])
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show("Despesa apagada com sucesso.", {
            severity: "success",
            autoHideDuration: 5000
          });
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar a despesa"
          );
        }
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar a despesa");
        stopLoading();
      });
  };

  const handleDeleteModal = () => {
    showWarning(
      "Tem a certeza que pretende apagar a despesa selecionada?",
      handleDeleteClick
    );
  };

  const breadcrumbs: Breadcrumb[] = [
    { title: "Despesas", path: baseUrl },
    ...(mode === IMode.ADD
      ? [{ title: "Novo" }]
      : mode === IMode.EDIT
        ? [
            { title: expense.id ?? "", path: `${baseUrl}/${expense.id ?? ""}` },
            { title: "Editar" }
          ]
        : [{ title: expense.id ?? "" }])
  ];

  return (
    <PageContainer
      title={
        mode === IMode.ADD
          ? "Criar Despesa"
          : mode === IMode.EDIT
            ? "Editar Despesa"
            : (expense.id ?? "")
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
        <ExpenseForm mode={mode} />
      </FormProvider>
    </PageContainer>
  );
}
