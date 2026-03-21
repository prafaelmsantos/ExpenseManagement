import Stack from "@mui/material/Stack";
import SignInPage from "../../pages/sign-in/SignInPage";
import SignInForm from "../../pages/sign-in/components/form/SignInForm";
import ThemeSwitcher from "../ThemeSwitcher";

export default function SignInLayout() {
  return (
    <Stack
      direction="column"
      component="main"
      sx={[
        {
          justifyContent: "center",
          height: "calc((1 - var(--template-frame-height, 0)) * 100%)",
          marginTop: "max(40px - var(--template-frame-height, 0px), 0px)",
          minHeight: "100%"
        },
        (theme) => ({
          "&::before": {
            content: '""',
            display: "block",
            position: "absolute",
            zIndex: -1,
            inset: 0,
            backgroundImage:
              "radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))",
            backgroundRepeat: "no-repeat",
            ...theme.applyStyles("dark", {
              backgroundImage:
                "radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))"
            })
          }
        })
      ]}
    >
      <Stack sx={{ position: "absolute", top: 16, right: 16, zIndex: 10 }}>
        <ThemeSwitcher />
      </Stack>
      <Stack
        direction={{ xs: "column-reverse", md: "row" }}
        sx={{
          justifyContent: "center",
          gap: { xs: 6, sm: 12 },
          p: 2,
          mx: "auto"
        }}
      >
        <Stack
          direction={{ xs: "column-reverse", md: "row" }}
          sx={{
            justifyContent: "center",
            gap: { xs: 6, sm: 12 },
            p: { xs: 2, sm: 4 },
            m: "auto"
          }}
        >
          <SignInPage />
          <SignInForm />
        </Stack>
      </Stack>
    </Stack>
  );
}
