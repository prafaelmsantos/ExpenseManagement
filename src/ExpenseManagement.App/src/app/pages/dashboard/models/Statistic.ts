export interface IYear {
  years: number[];
}

export interface IChart {
  name: string;
  amountTotal: number;
  data: number[];
}

export interface ISparkLineChart {
  name: string;
  amount: number;
  interval: number;
  trend: "up" | "down" | "neutral";
  data: number[];
}

export const StatisticLineLabels: string[] = [
  "jan.",
  "fev.",
  "mar.",
  "abr.",
  "mai.",
  "jun.",
  "jul.",
  "ago.",
  "set.",
  "out.",
  "nov.",
  "dez."
];
