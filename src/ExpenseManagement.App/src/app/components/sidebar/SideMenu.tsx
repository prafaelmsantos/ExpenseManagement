import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import OptionsMenu from "./OptionsMenu";
import useAuth from "../../context/useAuth/useAuth";

export default function SideMenu() {
  const { user } = useAuth();

  return (
    <Stack
      direction="row"
      sx={{
        p: 2,
        mt: 10,
        gap: 1,
        alignItems: "center",
        borderTop: "1px solid",
        borderColor: "divider"
      }}
    >
      <Avatar sizes="small" alt="Riley Carter" sx={{ width: 36, height: 36 }} />
      <Box sx={{ mr: "auto" }}>
        <Typography variant="caption" sx={{ color: "text.secondary" }}>
          Bem-vindo,
        </Typography>
        {user && (
          <Typography
            variant="body2"
            sx={{ fontWeight: 500, lineHeight: "16px" }}
          >
            {user.userName}
          </Typography>
        )}
      </Box>
      <OptionsMenu />
    </Stack>
  );
}
