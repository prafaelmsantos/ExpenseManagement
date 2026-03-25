import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Stack from "@mui/material/Stack";
import { BarChart } from "@mui/x-charts/BarChart";
import { IChart } from "../models/Statistic";
import { CategoryEnum, CategoryEnumPt } from "../../../models/Category";

interface IStatisticBarChart {
  title: string;
  subtitle: string;
  charts: IChart[];
}

export default function StatisticBarChart({
  title,
  subtitle,
  charts
}: IStatisticBarChart) {
  const labels = Object.values(CategoryEnum)
    .filter((v) => typeof v === "number")
    .sort((a, b) => (a as number) - (b as number))
    .map((v) => CategoryEnumPt[v as CategoryEnum]);

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
        <BarChart
          xAxis={[
            {
              scaleType: "band",
              data: labels,
              tickLabelStyle: {
                fontSize: 10
              }
            }
          ]}
          series={charts.map((x) => ({
            id: x.name,
            label: x.name,
            data: x.data
          }))}
          height={300}
        />
      </CardContent>
    </Card>
  );
}
