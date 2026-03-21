import { z } from "zod";

export const roleSchema = z.object({
  id: z.string().nullable(),
  name: z.string()
});

export const userSchema = z.object({
  id: z.string().nullable(),
  userName: z.string().trim().min(1, "Campo obrigatório."),
  firstName: z.string().nullable(),
  lastName: z.string().nullable(),
  role: roleSchema
});

export type IUserSchema = z.infer<typeof userSchema>;
