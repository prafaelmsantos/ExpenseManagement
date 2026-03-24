import SvgIcon from "@mui/material/SvgIcon";

export default function SitemarkIcon() {
  return (
    <SvgIcon sx={{ height: 28, width: 100, mr: 2 }}>
      {/* Cofrinho */}
      <rect x="1" y="8" width="6" height="10" fill="#00D3AB" rx="1" />
      {/* Moeda */}
      <circle cx="10" cy="13" r="4" fill="#4876EE" />
      {/* Gráfico */}
      <rect x="20" y="10" width="4" height="8" fill="#B4C0D3" />
      <rect x="26" y="6" width="4" height="12" fill="#4876EE" />
      <rect x="32" y="2" width="4" height="16" fill="#00D3AB" />
    </SvgIcon>
  );
}
