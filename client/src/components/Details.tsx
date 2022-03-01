import { Typography } from "@mui/material";
import React from "react";
import { IGameRes } from "../interfaces/GameRes";

const Details = ({ tries, result, change }: IGameRes) => {
  const formatResult = () =>
    `With ${tries} tries and changing is ${change} , your chanses of winning is ${result}%`;
  return <Typography variant="h3" gutterBottom sx={{ p: 2, justifyContent: 'center' , display: 'flex' }}>{formatResult()}</Typography>;
};

export default Details;
