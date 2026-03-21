import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import MuiCard from "@mui/material/Card";
import Checkbox from "@mui/material/Checkbox";
import FormControlLabel from "@mui/material/FormControlLabel";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { styled } from "@mui/material/styles";
import { Controller, useForm } from "react-hook-form";
import { ISignInSchema, signInSchema } from "../../services/SignInSchema";
import { useLoading } from "../../../../context/useLoading/useLoading";
import { zodResolver } from "@hookform/resolvers/zod";
import { IUserLogin } from "../../models/UserLogin";
import { postUserLogin } from "../../services/SignInService";
import { useModal } from "../../../../context/useModal/useModal";
import { IUserToken } from "../../models/UserToken";
import SitemarkIcon from "../../../../components/SitemarkIcon";
import useUser from "../../../../context/useUser/useUser";
import { IUser } from "../../../users/models/User";
import { getUserSettings } from "../../../users/services/UserService";

const Card = styled(MuiCard)(({ theme }) => ({
  display: "flex",
  flexDirection: "column",
  alignSelf: "center",
  width: "100%",
  padding: theme.spacing(4),
  gap: theme.spacing(2),
  boxShadow:
    "hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px",
  [theme.breakpoints.up("sm")]: {
    width: "450px"
  },
  ...theme.applyStyles("dark", {
    boxShadow:
      "hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px"
  })
}));

export default function SignInForm() {
  const { startLoading, stopLoading } = useLoading();
  const { setUser } = useUser();
  const { showError } = useModal();

  const methods = useForm<ISignInSchema>({
    resolver: zodResolver(signInSchema),
    mode: "all",
    reValidateMode: "onChange",
    shouldFocusError: true
  });
  const {
    handleSubmit,
    control,
    formState: { errors }
  } = methods;

  const handleUserLogin = async (userLogin: IUserLogin) => {
    startLoading();
    postUserLogin(userLogin)
      .then((userToken: IUserToken) => {
        localStorage.setItem("token", userToken.token);
        handleGetUserSettings();
        stopLoading();
        //navigate(`/products/${product.id}`);
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar entrar no sistema");
        stopLoading();
      });
  };

  const handleGetUserSettings = async () => {
    startLoading();
    getUserSettings()
      .then((user: IUser) => {
        setUser(user);
        stopLoading();
        //navigate(`/products/${product.id}`);
      })
      .catch((e: Error) => {
        showError(e.message, "Erro ao tentar entrar no sistema");
        stopLoading();
      });
  };

  return (
    <Card variant="outlined">
      <Box sx={{ display: { xs: "flex", md: "none" } }}>
        <SitemarkIcon />
      </Box>
      <Typography
        component="h1"
        variant="h4"
        sx={{ width: "100%", fontSize: "clamp(2rem, 10vw, 2.15rem)" }}
      >
        Expense Management System
      </Typography>
      <Box
        sx={{ display: "flex", flexDirection: "column", width: "100%", gap: 2 }}
      >
        <Controller
          name="userName"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <TextField
              {...field}
              label="Username"
              id="userName"
              autoFocus
              fullWidth
              variant="outlined"
              error={!!errors.userName}
              helperText={errors.userName?.message}
            />
          )}
        />
        <Controller
          name="password"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <TextField
              {...field}
              label="Password"
              id="password"
              fullWidth
              variant="outlined"
              error={!!errors.password}
              helperText={errors.password?.message}
            />
          )}
        />
        <FormControlLabel
          control={<Checkbox value="remember" color="primary" />}
          label="Remember me"
        />
        <Button
          type="submit"
          fullWidth
          variant="contained"
          onClick={handleSubmit(handleUserLogin)}
        >
          Iniciar sessão
        </Button>
      </Box>
    </Card>
  );
}
