import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { Controller, useFormContext } from "react-hook-form";
import { Divider, Grid, Paper } from "@mui/material";
import { IUserSettingsSchema } from "../../../services/UserSchema";
import { IMode } from "../../../../../models/Mode";

export default function UserSettingsForm({ mode }: { mode: IMode }) {
  const { control } = useFormContext<IUserSettingsSchema>();

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
      <Typography variant="h6" gutterBottom>
        Detalhes
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
    </Paper>
  );
}
