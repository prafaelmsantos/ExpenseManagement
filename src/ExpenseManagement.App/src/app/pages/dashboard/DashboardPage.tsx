import { Box, Grid, Stack } from "@mui/system";
import PageContainer from "../../components/PageContainer";
import StatisticSparkLineChart from "./components/StatisticSparkLineChart";
import { useModal } from "../../context/useModal/useModal";
import { useLoading } from "../../context/useLoading/useLoading";
import { IChart, ISparkLineChart, IYear } from "./models/Statistic";
import { useCallback, useEffect, useState } from "react";
import {
  getExpensesBarChart,
  getExpensesLineChart,
  getSavingsLineChart,
  getSparkLineChart,
  getYears
} from "./services/StatisticService";
import StatisticLineChart from "./components/StatisticLineChart";
import StatisticBarChart from "./components/StatisticBarChart";
import { Autocomplete, TextField } from "@mui/material";

export default function DashboardPage() {
  const { startLoading, stopLoading } = useLoading();
  const { showError } = useModal();

  const [year, setYear] = useState<IYear>({
    years: []
  });

  const [selectedYear, setSelectedYear] = useState<IYear>({
    years: [new Date().getFullYear()]
  });

  const [sparkLineChart, setSparkLineChart] = useState<ISparkLineChart[]>([]);

  const [expensesLineChart, setExpensesLineChart] = useState<IChart[]>([]);
  const [savingsLineChart, setSavingsLineChart] = useState<IChart[]>([]);

  const [expensesBarChart, setExpensesBarChart] = useState<IChart[]>([]);
  const [savingsBarChart, setSavingsBarChart] = useState<IChart[]>([]);

  const loadYearsData = useCallback(async () => {
    startLoading();
    getYears()
      .then((data) => {
        setYear(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar os anos.");
        stopLoading();
      });
  }, []);

  const loadSparkLineChart = useCallback(async () => {
    startLoading();
    getSparkLineChart()
      .then((data) => {
        setSparkLineChart(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar carregar os gráficos de poupanças e despesas."
        );
        stopLoading();
      });
  }, []);

  const loadExpensesLineChart = useCallback(async () => {
    startLoading();
    getExpensesLineChart(selectedYear)
      .then((data) => {
        setExpensesLineChart(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar carregar o gráfico de linhas da despesa."
        );
        stopLoading();
      });
  }, [selectedYear]);

  const loadSavingsLineChart = useCallback(async () => {
    startLoading();
    getSavingsLineChart(selectedYear)
      .then((data) => {
        setSavingsLineChart(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar carregar o gráfico de linhas da poupança."
        );
        stopLoading();
      });
  }, [selectedYear]);

  const loadExpensesBarChart = useCallback(async () => {
    startLoading();
    getExpensesBarChart(selectedYear)
      .then((data) => {
        setExpensesBarChart(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar carregar o gráfico de barras da despesa."
        );
        stopLoading();
      });
  }, [selectedYear]);

  const loadSavingsBarChart = useCallback(async () => {
    startLoading();
    getSavingsLineChart(selectedYear)
      .then((data) => {
        setSavingsBarChart(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(
          e.message,
          "Erro ao tentar carregar o gráfico de barras da poupança."
        );
        stopLoading();
      });
  }, [selectedYear]);

  useEffect(() => {
    void loadYearsData();
    void loadSparkLineChart();
  }, []);

  useEffect(() => {
    void loadExpensesLineChart();
    void loadSavingsLineChart();

    void loadExpensesBarChart();
    void loadSavingsBarChart();
  }, [selectedYear]);

  return (
    <PageContainer title={"Visão Geral"} breadcrumbs={[{ title: "Início" }]}>
      <Stack
        direction={{ xs: "column", sm: "row" }}
        justifyContent="end"
        alignItems={{ xs: "stretch", sm: "center" }}
        sx={{ mb: 2 }}
      >
        <Autocomplete
          multiple
          disablePortal
          options={year.years}
          value={selectedYear.years}
          onChange={(_, newValue) => setSelectedYear({ years: [...newValue] })}
          disableClearable
          size="small"
          sx={{ minWidth: 250 }}
          renderInput={(params) => (
            <TextField {...params} label="Anos" placeholder="Selecionar" />
          )}
        />
      </Stack>
      <Box sx={{ width: "100%", maxWidth: { sm: "100%", md: "1700px" } }}>
        <Grid
          container
          spacing={2}
          columns={12}
          sx={{ mb: (theme) => theme.spacing(2) }}
        >
          {sparkLineChart.map((card, index) => (
            <Grid key={index} size={{ xs: 12, sm: 6 }}>
              <StatisticSparkLineChart
                title={card.name}
                value={card.amount}
                interval={
                  card.interval > 1 ? `Últimos ${card.interval} dias` : `Hoje`
                }
                trend={card.trend}
                data={card.data}
              />
            </Grid>
          ))}
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticLineChart
              title="Poupanças"
              subtitle="(mês)"
              charts={savingsLineChart}
            />
          </Grid>
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticLineChart
              title="Despesas"
              subtitle="(mês)"
              charts={expensesLineChart}
            />
          </Grid>
          <Grid size={{ xs: 12 }}>
            <StatisticBarChart
              title="Poupanças"
              subtitle="(categoria)"
              charts={savingsBarChart}
            />
          </Grid>
          <Grid size={{ xs: 12 }}>
            <StatisticBarChart
              title="Despesas"
              subtitle="(categoria)"
              charts={expensesBarChart}
            />
          </Grid>
        </Grid>
      </Box>
    </PageContainer>
  );
}
