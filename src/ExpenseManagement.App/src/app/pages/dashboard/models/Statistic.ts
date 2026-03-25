export interface IYear {
  years: number[];
}

export interface ISeries {
  name: string;
  amountTotal: number;
  data: number[];
}

export interface IChart {
  labels: string[];
  series: ISeries[];
}
