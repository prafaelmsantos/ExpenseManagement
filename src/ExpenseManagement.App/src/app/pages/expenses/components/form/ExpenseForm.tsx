import { Controller, useFormContext } from "react-hook-form";
import {
  Autocomplete,
  Divider,
  InputAdornment,
  Paper,
  TextField,
  Typography
} from "@mui/material";
import Grid from "@mui/material/GridLegacy";
import { IMode } from "../../../../models/Mode";
import { IExpenseSchema } from "../../services/ExpenseSchema";
import { DateTimePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { ExpenseCategory, ExpenseCategoryPt } from "../../models/Expense";

export default function ExpenseForm({ mode }: { mode: IMode }) {
  const {
    control,
    formState: { errors }
  } = useFormContext<IExpenseSchema>();

  const categoryOptions = Object.values(ExpenseCategory).filter(
    (v) => typeof v === "number"
  ) as ExpenseCategory[];

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
                required
                label="Quantia"
                type="number"
                fullWidth
                variant="outlined"
                error={!!errors.amount}
                helperText={errors.amount?.message}
                onChange={(e) => field.onChange(Number(e.target.value))}
                slotProps={{
                  input: {
                    startAdornment: (
                      <InputAdornment position="start">€</InputAdornment>
                    ),
                    inputProps: { step: 0.1, min: 0.0 }
                  }
                }}
                disabled={mode == IMode.PREVIEW}
              />
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Controller
            name="category"
            control={control}
            defaultValue={ExpenseCategory.Housing}
            render={({ field }) => (
              <Autocomplete
                {...field}
                options={categoryOptions}
                getOptionLabel={(option) => ExpenseCategoryPt[option]}
                isOptionEqualToValue={(option, value) => option === value}
                onChange={(_, value) => field.onChange(value)}
                disabled={mode == IMode.PREVIEW}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    label="Categoria"
                    required={mode === IMode.ADD}
                    error={!!errors.category}
                    helperText={errors.category?.message}
                  />
                )}
              />
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Controller
            name="date"
            control={control}
            defaultValue=""
            render={({ field }) => (
              <DateTimePicker
                views={["year", "month", "day"]}
                label="Data"
                value={field.value ? dayjs(field.value) : null}
                onChange={(date) =>
                  field.onChange(
                    date ? date.startOf("day").toISOString() : null
                  )
                }
                ampm={false}
                disabled={mode == IMode.PREVIEW}
                localeText={{
                  cancelButtonLabel: "Cancelar",
                  okButtonLabel: "Confirmar"
                }}
              />
            )}
          />
        </Grid>
      </Grid>
    </Paper>
  );
}
