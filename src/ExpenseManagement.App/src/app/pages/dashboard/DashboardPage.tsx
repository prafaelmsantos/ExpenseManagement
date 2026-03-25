import { Box, Grid, Stack } from "@mui/system";
import PageContainer from "../../components/PageContainer";
import StatCard, { StatCardProps } from "./components/StatCard";
import { useModal } from "../../context/useModal/useModal";
import { useLoading } from "../../context/useLoading/useLoading";
import { IChart, IYear } from "./models/Statistic";
import { useCallback, useEffect, useState } from "react";
import {
  getExpensesByYears,
  getSavingsByYears,
  getYears
} from "./services/StatisticService";
import StatisticLineChart from "./components/StatisticLineChart";
import StatisticBarChart from "./components/StatisticBarChart";
import { Autocomplete, TextField, Typography } from "@mui/material";

export default function DashboardPage() {
  const data: StatCardProps[] = [
    {
      title: "Users",
      value: "14k",
      interval: "Last 30 days",
      trend: "up",
      data: [
        200, 24, 220, 260, 240, 380, 100, 240, 280, 240, 300, 340, 320, 360,
        340, 380, 360, 400, 380, 420, 400, 640, 340, 460, 440, 480, 460, 600,
        880, 920
      ]
    },
    {
      title: "Conversions",
      value: "325",
      interval: "Last 30 days",
      trend: "down",
      data: [
        1640, 1250, 970, 1130, 1050, 900, 720, 1080, 900, 450, 920, 820, 840,
        600, 820, 780, 800, 760, 380, 740, 660, 620, 840, 500, 520, 480, 400,
        360, 300, 220
      ]
    },
    {
      title: "Event count",
      value: "200k",
      interval: "Last 30 days",
      trend: "neutral",
      data: [
        500, 400, 510, 530, 520, 600, 530, 520, 510, 730, 520, 510, 530, 620,
        510, 530, 520, 410, 530, 520, 610, 530, 520, 610, 530, 420, 510, 430,
        520, 510
      ]
    },
    {
      title: "Event count",
      value: "200k",
      interval: "Last 30 days",
      trend: "neutral",
      data: [
        500, 400, 510, 530, 520, 600, 530, 520, 510, 730, 520, 510, 530, 620,
        510, 530, 520, 410, 530, 520, 610, 530, 520, 610, 530, 420, 510, 430,
        520, 510
      ]
    }
  ];

  const { startLoading, stopLoading } = useLoading();
  const { showError } = useModal();

  const [year, setYear] = useState<IYear>({
    years: []
  });

  const [selectedYear, setSelectedYear] = useState<IYear>({
    years: [new Date().getFullYear()]
  });

  const [chartExpensesByYears, setChartExpensesByYears] = useState<IChart>({
    labels: [],
    series: []
  });

  const [chartSavingsByYears, setChartSavingsByYears] = useState<IChart>({
    labels: [],
    series: []
  });

  const loadDataGetYears = useCallback(async () => {
    startLoading();
    getYears()
      .then((data) => {
        setYear(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar despesas por ano.");
        stopLoading();
      });
  }, []);

  const loadDataGetExpensesByYears = useCallback(async () => {
    startLoading();
    getExpensesByYears(selectedYear)
      .then((data) => {
        setChartExpensesByYears(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar despesas por ano.");
        stopLoading();
      });
  }, [selectedYear]);

  const loadDataGetSavingsByYears = useCallback(async () => {
    startLoading();
    getSavingsByYears(selectedYear)
      .then((data) => {
        setChartSavingsByYears(data);
        stopLoading();
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar carregar poupanças por ano.");
        stopLoading();
      });
  }, [selectedYear]);

  useEffect(() => {
    void loadDataGetYears();
  }, []);

  useEffect(() => {
    void loadDataGetExpensesByYears();
    void loadDataGetSavingsByYears();
  }, [selectedYear]);

  return (
    <PageContainer title={"Início"} breadcrumbs={[{ title: "Início" }]}>
      <Stack
        direction={{ xs: "column", sm: "row" }}
        justifyContent="space-between"
        alignItems={{ xs: "stretch", sm: "center" }}
        spacing={2}
        sx={{ mb: 2 }}
      >
        <Typography variant="h6">Visão Geral</Typography>

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
          {data.map((card, index) => (
            <Grid key={index} size={{ xs: 12, sm: 6, lg: 3 }}>
              <StatCard {...card} />
            </Grid>
          ))}
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticLineChart title="Poupanças" chart={chartSavingsByYears} />
          </Grid>
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticLineChart title="Despesas" chart={chartExpensesByYears} />
          </Grid>
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticBarChart title="Poupanças" chart={chartSavingsByYears} />
          </Grid>
          <Grid size={{ xs: 12, md: 6 }}>
            <StatisticBarChart title="Despesas" chart={chartExpensesByYears} />
          </Grid>
        </Grid>
      </Box>
    </PageContainer>
  );
}
