import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    type: "dark",
    primary: {
      main: "#664FBA !important",
    },
    secondary: {
      main: "#F97D7D !important",
    },
    neutral: {
      main: "#ECF3FE !important",
    },
    background: {
      default: "#22272A !important",
    },
    text: {
      primary: "#ECF3FE !important",
      secondary: "#bdb3e6 !important",
    },
  },
  components: {
    MuiIconButton: {
      styleOverrides: {
        root: {
          primary: "#ECF3FE !important",
        },
      },
    },
    MuiTab: {
      styleOverrides: {
        root: {
          padding: "0.5rem !important",
        },
      },
    },
    MuiTabs: {
      styleOverrides: {
        scroller: {
          marginBottom: "2rem !important",
          marginTop: "0.5rem !important",
        },
      },
    },
  },
});

export default theme;
