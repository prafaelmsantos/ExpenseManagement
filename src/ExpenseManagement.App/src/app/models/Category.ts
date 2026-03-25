export enum CategoryEnum {
  Housing = 1,
  Food = 2,
  Transports = 3,
  Car = 4,
  Health = 5,
  Entertainment = 6,
  Education = 7,
  Utilities = 8,
  Insurance = 9,
  Taxes = 10,
  Shopping = 11,
  Travel = 12,
  Investments = 13,
  Subscriptions = 14,
  Gifts = 15,
  Other = 99
}

export const CategoryEnumPt: Record<CategoryEnum, string> = {
  [CategoryEnum.Housing]: "Habitação",
  [CategoryEnum.Food]: "Alimentação",
  [CategoryEnum.Transports]: "Transportes",
  [CategoryEnum.Car]: "Carro",
  [CategoryEnum.Health]: "Saúde",
  [CategoryEnum.Entertainment]: "Entretenimento",
  [CategoryEnum.Education]: "Educação",
  [CategoryEnum.Utilities]: "Serviços",
  [CategoryEnum.Insurance]: "Seguros",
  [CategoryEnum.Taxes]: "Impostos",
  [CategoryEnum.Shopping]: "Compras",
  [CategoryEnum.Travel]: "Viagens",
  [CategoryEnum.Investments]: "Investimentos",
  [CategoryEnum.Subscriptions]: "Subscrições",
  [CategoryEnum.Gifts]: "Presentes",
  [CategoryEnum.Other]: "Outros"
};
