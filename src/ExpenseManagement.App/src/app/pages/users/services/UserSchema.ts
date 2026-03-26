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

export const userPasswordSchema = z.object({
  currentPassword: z
    .string()
    .trim()
    .min(6, "A palavra-passe tem de ter pelo menor 6 digitos/caracteres."),
  newPassword: z
    .string()
    .trim()
    .min(6, "A palavra-passe tem de ter pelo menor 6 digitos/caracteres.")
});

export type IUserPasswordSchema = z.infer<typeof userPasswordSchema>;

export const userSettingsSchema = z.object({
  firstName: z.string().nullable(),
  lastName: z.string().nullable()
});

export type IUserSettingsSchema = z.infer<typeof userSettingsSchema>;
