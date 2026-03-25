import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Stack from "@mui/material/Stack";
import { BarChart } from "@mui/x-charts/BarChart";
import { IChart } from "../models/Statistic";

interface IStatisticBarChart {
  title: string;
  chart: IChart;
}

export default function StatisticBarChart({
  title,
  chart: lineChart
}: IStatisticBarChart) {
  return (
    <Card variant="outlined" sx={{ width: "100%" }}>
      <CardContent>
        <Typography component="h2" variant="subtitle2" gutterBottom>
          {title}
        </Typography>
        <Stack
          direction="row"
          sx={{
            alignContent: { xs: "center", sm: "flex-start" },
            alignItems: "center",
            gap: 1
          }}
        >
          {lineChart.series.map((x, index) => (
            <Typography
              key={index}
              variant="caption"
              sx={{ color: "text.secondary" }}
            >
              {`Total (${x.name}): €${x.amountTotal}`}
            </Typography>
          ))}
        </Stack>

        <BarChart
          localeText={{
            noData: "Sem dados para exibir"
          }}
          borderRadius={8}
          xAxis={[
            {
              scaleType: "band",
              categoryGapRatio: 0.5,
              data: lineChart.labels,
              height: 24
            }
          ]}
          yAxis={[{ width: 50 }]}
          series={lineChart.series.map((x) => ({
            id: x.name,
            label: x.name,
            showMark: false,
            stack: "A",
            data: x.data
          }))}
          height={250}
          margin={{ left: 0, right: 0, top: 20, bottom: 0 }}
          grid={{ horizontal: true }}
        />
      </CardContent>
    </Card>
  );
}
