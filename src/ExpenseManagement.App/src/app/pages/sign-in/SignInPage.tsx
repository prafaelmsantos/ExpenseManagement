import Box from "@mui/material/Box";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import SitemarkIcon from "../../components/SitemarkIcon";

import {
  AccountBalanceWalletRounded as WalletIcon,
  BarChartRounded as DashboardIcon,
  AttachMoneyRounded as MoneyIcon,
  AutoFixHighRounded as InnovationIcon
} from "@mui/icons-material";

const items = [
  {
    icon: <DashboardIcon sx={{ color: "text.secondary" }} />,
    title: "Dashboard com Gráficos",
    description:
      "Visualize suas despesas e poupanças em gráficos claros e interativos, para entender melhor o seu fluxo financeiro."
  },
  {
    icon: <WalletIcon sx={{ color: "text.secondary" }} />,
    title: "Gestão de Poupanças",
    description:
      "Acompanhe e organize suas poupanças de forma simples e eficiente, mantendo o controlo total sobre o seu dinheiro."
  },
  {
    icon: <MoneyIcon sx={{ color: "text.secondary" }} />,
    title: "Controle de Despesas",
    description:
      "Registe todas as suas despesas e visualize relatórios detalhados para tomar decisões financeiras mais inteligentes."
  },
  {
    icon: <InnovationIcon sx={{ color: "text.secondary" }} />,
    title: "Funcionalidades Inteligentes",
    description:
      "Explore ferramentas inovadoras que ajudam a automatizar tarefas e otimizar o fluxo financeiro do seu negócio."
  }
];

export default function SignInPage() {
  return (
    <Stack
      sx={{
        flexDirection: "column",
        alignSelf: "center",
        gap: 4,
        maxWidth: 450
      }}
    >
      <Box sx={{ display: { xs: "none", md: "flex" } }}>
        <SitemarkIcon />
      </Box>
      {items.map((item, index) => (
        <Stack key={index} direction="row" sx={{ gap: 2 }}>
          {item.icon}
          <div>
            <Typography gutterBottom sx={{ fontWeight: "medium" }}>
              {item.title}
            </Typography>
            <Typography variant="body2" sx={{ color: "text.secondary" }}>
              {item.description}
            </Typography>
          </div>
        </Stack>
      ))}
    </Stack>
  );
}
