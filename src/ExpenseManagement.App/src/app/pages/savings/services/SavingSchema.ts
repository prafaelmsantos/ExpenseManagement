import { z } from "zod";
import { CategoryEnum } from "../../../models/Category";

export const savingSchema = z.object({
  id: z.string().nullable(),
  name: z.string().trim().min(1, "Campo obrigatório."),
  amount: z.number().min(0, "Campo obrigatório."),
  category: z.enum(CategoryEnum, "Campo obrigatório."),
  date: z.string().trim().min(1, "Campo obrigatório."),
  description: z.string().nullable()
});

export type ISavingSchema = z.infer<typeof savingSchema>;
