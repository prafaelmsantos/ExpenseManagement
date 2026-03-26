import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { Controller, useFormContext } from "react-hook-form";
import {
  Divider,
  Grid,
  IconButton,
  InputAdornment,
  Paper
} from "@mui/material";
import { IUserPasswordSchema } from "../../users/services/UserSchema";
import { IMode } from "../../../models/Mode";
import { useState } from "react";
import { Visibility, VisibilityOff } from "@mui/icons-material";

export default function UserSettingsForm({ mode }: { mode: IMode }) {
  const [showCurrentPassword, setShowCurrentPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const {
    control,
    formState: { errors }
  } = useFormContext<IUserPasswordSchema>();

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
      <Typography variant="h6" gutterBottom>
        Alterar Palavra-passe
      </Typography>
      <Divider sx={{ mb: 5 }} />

      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 6 }}>
          <Controller
            name="currentPassword"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                label="Atual Palavra-passe"
                fullWidth
                type={showCurrentPassword ? "text" : "password"}
                variant="outlined"
                error={!!errors.currentPassword}
                helperText={errors.currentPassword?.message}
                disabled={mode == IMode.PREVIEW}
                slotProps={{
                  input: {
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton
                          onClick={() =>
                            setShowCurrentPassword((prev) => !prev)
                          }
                          disabled={mode == IMode.PREVIEW}
                          edge="end"
                        >
                          {showCurrentPassword ? (
                            <VisibilityOff />
                          ) : (
                            <Visibility />
                          )}
                        </IconButton>
                      </InputAdornment>
                    )
                  }
                }}
              />
            )}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <Controller
            name="newPassword"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                label="Nova Palavra-passe"
                fullWidth
                type={showNewPassword ? "text" : "password"}
                variant="outlined"
                error={!!errors.newPassword}
                helperText={errors.newPassword?.message}
                disabled={mode == IMode.PREVIEW}
                slotProps={{
                  input: {
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton
                          onClick={() => setShowNewPassword((prev) => !prev)}
                          disabled={mode == IMode.PREVIEW}
                          edge="end"
                        >
                          {showNewPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                      </InputAdornment>
                    )
                  }
                }}
              />
            )}
          />
        </Grid>
      </Grid>
    </Paper>
  );
}
