import { Navigate, useRoutes } from "react-router";
import Dashboard from "../pages/dashboard/DashboardPage";
import Layout from "../components/layout/Layout";
import SignInLayout from "../components/layout/SignInLayout";
import useUser from "../context/useUser/useUser";
import UserPage from "../pages/users/UserPage";
import UsersPage from "../pages/users/UsersPage";

export default function AppRouter() {
  const { user } = useUser();

  const Routes = user
    ? [
        {
          element: <Layout />,
          children: [
            { path: "/", element: <Dashboard /> },

            { path: "/users", element: <UsersPage /> },
            { path: "/users/:userId", element: <UserPage /> },
            { path: "/users/:userId/edit", element: <UserPage /> },
            { path: "/users/new", element: <UserPage /> },
            { path: "*", element: <Navigate to="/" replace /> }
          ]
        }
      ]
    : [
        {
          element: <SignInLayout />,
          children: [
            { path: "/", element: <SignInLayout /> },
            { path: "*", element: <Navigate to="/" replace /> }
          ]
        }
      ];

  return useRoutes(Routes);
}
