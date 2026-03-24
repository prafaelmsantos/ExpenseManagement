import { Controller, useFormContext } from "react-hook-form";
import { Divider, Grid, Paper, TextField, Typography } from "@mui/material";
import { IUserSchema } from "../../services/UserSchema";
import { IMode } from "../../../../models/Mode";

export default function UserForm({ mode }: { mode: IMode }) {
  const {
    control,
    formState: { errors }
  } = useFormContext<IUserSchema>();

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
      <Typography variant="h6" gutterBottom>
        Detalhes do Utilizador
      </Typography>
      <Divider sx={{ mb: 5 }} />

      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 6 }}>
          <Controller
            name="firstName"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                label="Primeiro Nome"
                fullWidth
                variant="outlined"
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <Controller
            name="lastName"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                label="Último Nome"
                fullWidth
                variant="outlined"
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>
      </Grid>
      <Grid container spacing={2} sx={{ mt: 2 }}>
        <Grid size={{ xs: 12, sm: 6 }}>
          <Controller
            name="userName"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                required={mode == IMode.ADD}
                label="Nome do Utilizador"
                fullWidth
                variant="outlined"
                error={!!errors.userName}
                helperText={errors.userName?.message}
                disabled={mode == IMode.PREVIEW || mode == IMode.EDIT}
              />
            )}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          {mode !== IMode.ADD && (
            <Controller
              name="role.name"
              control={control}
              defaultValue=""
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Cargo"
                  fullWidth
                  variant="outlined"
                  disabled
                />
              )}
            />
          )}
        </Grid>
      </Grid>
    </Paper>
  );
}
