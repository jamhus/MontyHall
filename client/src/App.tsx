import {
  Box,
  Checkbox,
  Container,
  FormControlLabel,
  TextField,
} from "@mui/material";
import { LoadingButton } from "@mui/lab";
import axios from "axios";
import { useState } from "react";
import Details from "./components/Details";
import { IGameRes } from "./interfaces/GameRes";

function App() {
  const [tries, setTries] = useState<number>(0);
  const [change, setChange] = useState(false);
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setSubmitting] = useState(false);
  const [currentResult, setCurrentResult] = useState<IGameRes>({
    tries: 0,
    result: 0,
    change: false,
  });

  const handleSubmit = () => {
    setLoading(true);
    setSubmitting(true);
    axios
      .post<IGameRes>("https://localhost:7170/api/Game", {
        tries,
        change,
      })
      .then((res) => {
        setCurrentResult({ ...res.data, change });
        setLoading(false);
        setSubmitting(false);
      })
      .catch((err) => console.log(err, "error"));
  };
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {

    setChange(event.target.checked);
  };
  return (
    <Container sx={{ height: 400 }}>
      <Box sx={{ mt: 1, display: "flex", justifyContent: "center" }}>
        <TextField
          margin="normal"
          label="tries"
          type="number"
          autoFocus
          value={tries}
          onChange={(e) => {
            const tries = parseInt(e.target.value);
            if(tries > 0){
              setTries(parseInt(e.target.value))
            }
          }}
          sx={{ mr: 5 }}
        />

        <FormControlLabel
          control={
            <Checkbox
              checked={change}
              onChange={handleChange}
              inputProps={{ "aria-label": "controlled" }}
            />
          }
          label="Change"
        />

        <LoadingButton
          loading={isSubmitting}
          type="button"
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
          onClick={handleSubmit}
        >
          Send
        </LoadingButton>
      </Box>
      {!loading && <Details {...currentResult} />}
    </Container>
  );
}

export default App;
