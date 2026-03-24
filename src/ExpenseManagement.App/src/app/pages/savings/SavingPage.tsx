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
import dayjs from "dayjs";
import { CategoryEnum } from "../../models/Category";
import { ISavingSchema, savingSchema } from "./services/SavingSchema";
import { ISaving } from "./models/Saving";
import {
  createSaving,
  deleteSavings,
  getSaving,
  updateSaving
} from "./services/SavingService";
import SavingForm from "./components/form/SavingForm";

export default function SavingPage() {
  const baseUrl: string = "/savings";

  const navigate = useNavigate();
  const params = useParams<{ savingId: string }>();
  const savingId = params.savingId;

  const notifications = useNotifications();
  const { startLoading, stopLoading } = useLoading();
  const { showError, showWarning } = useModal();

  const methods = useForm<ISavingSchema>({
    resolver: zodResolver(savingSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });

  const { reset, handleSubmit } = methods;

  const [saving, setSaving] = useState<ISaving>({
    id: null,
    name: "",
    amount: 0,
    category: CategoryEnum.Housing,
    date: dayjs().startOf("day").toISOString(),
    description: ""
  });

  const [mode, setMode] = useState<IMode>(IMode.PREVIEW);

  const matchNew = useMatch({ path: "/savings/new", end: true });
  const matchEdit = useMatch({
    path: "/savings/:savingId/edit",
    end: true
  });
  const matchDetail = useMatch({ path: "/savings/:savingId", end: true });

  useEffect(() => {
    if (matchNew) setMode(IMode.ADD);
    else if (matchEdit) setMode(IMode.EDIT);
    else if (matchDetail) setMode(IMode.PREVIEW);
  }, [matchNew, matchEdit, matchDetail]);

  const loadData = useCallback(async () => {
    if (savingId) {
      startLoading();
      getSaving(savingId)
        .then((data) => {
          setSaving(data);
          stopLoading();
        })
        .catch((e: Error) => {
          void handleClose();
          showError(e.message, "Erro ao tentar carregar a poupança");
          stopLoading();
        });
    } else if (!matchNew) {
      void handleClose();
    }
  }, [savingId]);

  useEffect(() => {
    void loadData();
  }, [savingId]);

  useEffect(() => {
    void reset(saving);
  }, [saving]);

  const handleClose = () => navigate(baseUrl);

  const handleSumbitEdit = async (saving: ISaving) => {
    startLoading();
    updateSaving(saving)
      .then(() => {
        notifications.show("Despesa atualizada com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });

        navigate(`/savings/${saving.id}`);
        void loadData();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar atualizar a poupança");
        stopLoading();
      });
  };

  const handleSumbitAdd = async (saving: ISaving) => {
    startLoading();
    createSaving(saving)
      .then(() => {
        notifications.show("Poupança criada com sucesso!", {
          severity: "success",
          autoHideDuration: 5000
        });
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar criar a poupança");
        stopLoading();
      });
  };

  const handleEdit = () => {
    console.log(savingId);
    navigate(`/savings/${savingId}/edit`);
  };

  const handleRollback = () => {
    navigate(`/savings/${savingId}`);
    void loadData();
  };

  const handleDeleteClick = () => {
    startLoading();
    deleteSavings([savingId ?? ""])
      .then((data) => {
        const allSuccess = data.every((x) => x.success);
        if (allSuccess) {
          notifications.show("Poupança apagada com sucesso.", {
            severity: "success",
            autoHideDuration: 5000
          });
        } else {
          showError(
            data.map((x) => x.message).join("\n"),
            "Houve um erro ao tentar apagar a poupança"
          );
        }
        void handleClose();
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Houve um erro ao tentar apagar a poupança");
        stopLoading();
      });
  };

  const handleDeleteModal = () => {
    showWarning(
      "Tem a certeza que pretende apagar a poupança selecionada?",
      handleDeleteClick
    );
  };

  const breadcrumbs: Breadcrumb[] = [
    { title: "Poupanças", path: baseUrl },
    ...(mode === IMode.ADD
      ? [{ title: "Novo" }]
      : mode === IMode.EDIT
        ? [
            { title: saving.id ?? "", path: `${baseUrl}/${saving.id ?? ""}` },
            { title: "Editar" }
          ]
        : [{ title: saving.id ?? "" }])
  ];

  return (
    <PageContainer
      title={
        mode === IMode.ADD
          ? "Criar Poupança"
          : mode === IMode.EDIT
            ? "Editar Poupança"
            : (saving.id ?? "")
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
            onClick={
              mode === IMode.PREVIEW
                ? handleEdit
                : handleSubmit(
                    mode === IMode.ADD ? handleSumbitAdd : handleSumbitEdit
                  )
            }
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
        <SavingForm disabled={mode === IMode.PREVIEW} />
      </FormProvider>
    </PageContainer>
  );
}
