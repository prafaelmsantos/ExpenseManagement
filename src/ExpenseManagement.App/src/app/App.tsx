import AppTheme from "./theme/AppTheme";
import NotificationsProvider from "./context/useNotifications/NotificationsProvider";
import CssBaseline from "@mui/material/CssBaseline";
import AppRouter from "./routes/AppRouter";
import { LoadingProvider } from "./context/useLoading/LoadingProvider";
import { ModalProvider } from "./context/useModal/ModalProvider";
import { AuthProvider } from "./context/useAuth/AuthProvider";

function App() {
  return (
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
  );
}

export default App;
