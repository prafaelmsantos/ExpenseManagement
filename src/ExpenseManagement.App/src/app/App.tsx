import AppTheme from "./theme/AppTheme";
import NotificationsProvider from "./context/useNotifications/NotificationsProvider";
import CssBaseline from "@mui/material/CssBaseline";
import AppRouter from "./routes/AppRouter";
import { LoadingProvider } from "./context/useLoading/LoadingProvider";
import { ModalProvider } from "./context/useModal/ModalProvider";
import { AuthProvider } from "./context/useAuth/AuthProvider";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import "dayjs/locale/pt";

function App() {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="pt">
      <AppTheme>
        <CssBaseline enableColorScheme />
        <AuthProvider>
          <NotificationsProvider>
            <LoadingProvider>
              <ModalProvider>
                <AppRouter />
              </ModalProvider>
            </LoadingProvider>
          </NotificationsProvider>
        </AuthProvider>
      </AppTheme>
    </LocalizationProvider>
  );
}

export default App;
