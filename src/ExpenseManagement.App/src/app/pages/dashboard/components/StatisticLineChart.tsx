import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Stack from "@mui/material/Stack";
import { LineChart } from "@mui/x-charts/LineChart";
import { IChart, StatisticLineLabels } from "../models/Statistic";

interface IStatisticLineChart {
  title: string;
  subtitle: string;
  charts: IChart[];
}

export default function StatisticLineChart({
  title,
  subtitle,
  charts
}: IStatisticLineChart) {
  return (
    <Card variant="outlined" sx={{ width: "100%" }}>
      <CardContent>
        <Typography component="h2" variant="subtitle2" gutterBottom>
          {title}
          <Typography variant="caption" sx={{ color: "text.secondary", mx: 1 }}>
            {subtitle}
          </Typography>
        </Typography>

        <Stack
          direction="row"
          sx={{
            alignContent: { xs: "center", sm: "flex-start" },
            alignItems: "center",
            gap: 1
          }}
        >
          {charts.map((x, index) => (
            <Typography
              key={index}
              variant="caption"
              sx={{ color: "text.secondary" }}
            >
              {`Total (${x.name}): ${x.amountTotal.toLocaleString("pt-PT", {
                style: "currency",
                currency: "EUR"
              })}`}
            </Typography>
          ))}
        </Stack>

        <LineChart
          localeText={{
            noData: "Sem dados para exibir"
          }}
          xAxis={[
            {
              scaleType: "point",
              data: StatisticLineLabels,
              height: 24
            }
          ]}
          yAxis={[{ width: 50 }]}
          series={charts.map((x) => ({
            id: x.name,
            label: x.name,
            showMark: false,
            //area: true,
            data: x.data
          }))}
          height={250}
          margin={{ left: 0, right: 20, top: 20, bottom: 0 }}
          grid={{ horizontal: true }}
        ></LineChart>
      </CardContent>
    </Card>
  );
}
