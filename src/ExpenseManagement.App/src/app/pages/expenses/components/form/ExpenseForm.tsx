import { Controller, useFormContext } from "react-hook-form";
import { Divider, Paper, TextField, Typography } from "@mui/material";
import Grid from "@mui/material/GridLegacy";
import { IMode } from "../../../../models/Mode";
import { IExpenseSchema } from "../../services/ExpenseSchema";
import { DateTimePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";

export default function ExpenseForm({ mode }: { mode: IMode }) {
  const {
    control,
    formState: { errors }
  } = useFormContext<IExpenseSchema>();

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
      <Typography variant="h6" gutterBottom>
        Detalhes da Despesa
      </Typography>
      <Divider sx={{ mb: 5 }} />

      <Grid container spacing={2}>
        <Grid item xs={12} sm={6}>
          <Controller
            name="amount"
            control={control}
            defaultValue={0}
            render={({ field }) => (
              <TextField
                {...field}
                label="Quantia"
                fullWidth
                type="number"
                variant="outlined"
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Controller
            name="category"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <TextField
                {...field}
                label="Categoria"
                fullWidth
                variant="outlined"
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Controller
            name="date"
            control={control}
            defaultValue={new Date()}
            render={({ field }) => (
              <DateTimePicker
                label="Data"
                value={field.value ? dayjs(field.value) : null}
                onChange={(date) => field.onChange(date?.toISOString())}
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>
      </Grid>
    </Paper>
  );
}
