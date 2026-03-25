import * as React from "react";
import { type Theme, SxProps } from "@mui/material/styles";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import Collapse from "@mui/material/Collapse";
import Grow from "@mui/material/Grow";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemText from "@mui/material/ListItemText";
import Paper, { paperClasses } from "@mui/material/Paper";
import type {} from "@mui/material/themeCssVarsAugmentation";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { useNavigate } from "react-router";
import DashboardSidebarContext from "../../context/DashboardSidebarContext";
import { MINI_DRAWER_WIDTH } from "../Constants";
import useAuth from "../../context/useAuth/useAuth";
import ListItemIcon, { listItemIconClasses } from "@mui/material/ListItemIcon";
import LogoutRoundedIcon from "@mui/icons-material/LogoutRounded";
import {
  Divider,
  dividerClasses,
  listClasses,
  Menu,
  MenuItem,
  Typography
} from "@mui/material";

export interface SidebarPageItemProps {
  id: string;
  href: string;
  action?: React.ReactNode;
  defaultExpanded?: boolean;
  expanded?: boolean;
  selected?: boolean;
  disabled?: boolean;
  nestedNavigation?: React.ReactNode;
}

export default function SidebarUserPageItem({
  id,
  href,
  action,
  defaultExpanded = false,
  expanded = defaultExpanded,
  selected = false,
  disabled = false,
  nestedNavigation
}: SidebarPageItemProps) {
  const sidebarContext = React.useContext(DashboardSidebarContext);
  if (!sidebarContext) {
    throw new Error("Sidebar context was used without a provider.");
  }
  const {
    onPageItemClick,
    mini = false,
    fullyExpanded = true,
    fullyCollapsed = false
  } = sidebarContext;

  const [isHovered, setIsHovered] = React.useState(false);

  const handleClick = React.useCallback(
    (event: React.MouseEvent<HTMLElement>) => {
      if (onPageItemClick) {
        onPageItemClick(id, !!nestedNavigation);
        setAnchorEl(event.currentTarget);
      }
    },
    [onPageItemClick, id, nestedNavigation]
  );

  const navigate = useNavigate();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const { clearUser, user } = useAuth();
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleNavigate = () => {
    navigate(href);
    setAnchorEl(null);
  };

  const handleLogout = () => {
    setAnchorEl(null);
    clearUser();
  };

  let nestedNavigationCollapseSx: SxProps<Theme> = { display: "none" };
  if (mini && fullyCollapsed) {
    nestedNavigationCollapseSx = {
      fontSize: 18,
      position: "absolute",
      top: "41.5%",
      right: "2px",
      transform: "translateY(-50%) rotate(-90deg)"
    };
  } else if (!mini && fullyExpanded) {
    nestedNavigationCollapseSx = {
      ml: 0.5,
      fontSize: 20,
      transform: `rotate(${expanded ? 0 : -90}deg)`,
      transition: (theme: Theme) =>
        theme.transitions.create("transform", {
          easing: theme.transitions.easing.sharp,
          duration: 100
        })
    };
  }

  const miniNestedNavigationSidebarContextValue = React.useMemo(() => {
    return {
      onPageItemClick: onPageItemClick ?? (() => {}),
      mini: false,
      fullyExpanded: true,
      fullyCollapsed: false,
      hasDrawerTransitions: false
    };
  }, [onPageItemClick]);

  return (
    <React.Fragment>
      <Menu
        anchorEl={anchorEl}
        id="menu"
        open={open}
        onClose={handleClose}
        transformOrigin={{ horizontal: "right", vertical: "top" }}
        anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
        sx={{
          [`& .${listClasses.root}`]: {
            padding: "4px"
          },
          [`& .${paperClasses.root}`]: {
            padding: 0
          },
          [`& .${dividerClasses.root}`]: {
            margin: "4px -4px"
          }
        }}
      >
        <MenuItem onClick={handleNavigate}>Definições</MenuItem>
        <Divider />
        <MenuItem
          onClick={handleLogout}
          sx={{
            [`& .${listItemIconClasses.root}`]: {
              ml: "auto",
              minWidth: 0
            }
          }}
        >
          <ListItemText>Sair</ListItemText>
          <ListItemIcon>
            <LogoutRoundedIcon fontSize="small" />
          </ListItemIcon>
        </MenuItem>
      </Menu>
      <ListItem
        disablePadding
        {...(nestedNavigation && mini
          ? {
              onMouseEnter: () => {
                setIsHovered(true);
              },
              onMouseLeave: () => {
                setIsHovered(false);
              }
            }
          : {})}
        sx={{
          display: "block",
          py: 0,
          px: 1,
          overflowX: "hidden"
        }}
      >
        <ListItemButton
          selected={selected}
          disabled={disabled}
          sx={{
            height: mini ? 50 : "auto"
          }}
          {...(nestedNavigation && !mini
            ? {
                onClick: handleClick
              }
            : {})}
          {...(!nestedNavigation
            ? {
                onClick: handleClick
              }
            : {})}
        >
          {mini ? (
            <Box
              sx={{
                position: "absolute",
                left: "50%",
                top: "calc(50%)",
                transform: "translate(-50%, -50%)"
              }}
            >
              <ListItemIcon
                sx={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: mini ? "center" : "auto"
                }}
              >
                <Avatar
                  sizes="small"
                  alt="Riley Carter"
                  sx={{ width: 36, height: 36 }}
                />
              </ListItemIcon>
            </Box>
          ) : (
            <>
              <ListItemIcon
                sx={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: mini ? "center" : "auto"
                }}
              >
                <Avatar
                  sizes="small"
                  alt="Riley Carter"
                  sx={{ width: 36, height: 36 }}
                />
              </ListItemIcon>
              <ListItemText
                primary={
                  <Box sx={{ mr: "auto" }}>
                    <Typography
                      variant="caption"
                      sx={{ color: "text.secondary" }}
                    >
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
                }
                sx={{
                  whiteSpace: "nowrap",
                  zIndex: 1
                }}
              />
            </>
          )}

          {action && !mini && fullyExpanded ? action : null}
          {nestedNavigation ? (
            <ExpandMoreIcon sx={nestedNavigationCollapseSx} />
          ) : null}
        </ListItemButton>
        {nestedNavigation && mini ? (
          <Grow in={isHovered}>
            <Box
              sx={{
                position: "fixed",
                left: MINI_DRAWER_WIDTH - 2,
                pl: "6px"
              }}
            >
              <Paper
                elevation={8}
                sx={{
                  pt: 0.2,
                  pb: 0.2,
                  transform: "translateY(-50px)"
                }}
              >
                <DashboardSidebarContext.Provider
                  value={miniNestedNavigationSidebarContextValue}
                >
                  {nestedNavigation}
                </DashboardSidebarContext.Provider>
              </Paper>
            </Box>
          </Grow>
        ) : null}
      </ListItem>
      {nestedNavigation && !mini ? (
        <Collapse in={expanded} timeout="auto" unmountOnExit>
          {nestedNavigation}
        </Collapse>
      ) : null}
    </React.Fragment>
  );
}
