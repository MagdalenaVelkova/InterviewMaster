import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    type: "dark",
    primary: {
      main: "#664FBA",
    },
    secondary: {
      main: "#F97D7D",
    },
    background: {
      default: "#22272A",
    },
    text: {
      primary: "#ECF3FE !important",
      secondary: "#bdb3e6 !important",
    },
  },
  components: {
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
