import AppTheme from "./theme/AppTheme";
import NotificationsProvider from "./context/useNotifications/NotificationsProvider";
import CssBaseline from "@mui/material/CssBaseline";
import AppRouter from "./routes/AppRouter";
import { LoadingProvider } from "./context/useLoading/LoadingProvider";
import { ModalProvider } from "./context/useModal/ModalProvider";
import { UserProvider } from "./context/useUser/UserProvider";

function App() {
  return (
    <AppTheme>
      <CssBaseline enableColorScheme />
      <UserProvider>
        <NotificationsProvider>
          <LoadingProvider>
            <ModalProvider>
              <AppRouter />
            </ModalProvider>
          </LoadingProvider>
        </NotificationsProvider>
      </UserProvider>
    </AppTheme>
  );
}

export default App;
