import React from "react";
import { render, screen } from "@testing-library/react";
import Details from "./components/Details";
import { IGameRes } from "./interfaces/GameRes";

test("If ParentComponent is passed data, ChildComponent is called with prop open and data", () => {
  const data: IGameRes = {
    tries: 100,
    result: 50,
    change: true,
  };
  render(<Details {...data} />);

  const result = screen.getByTestId("result");
  expect(result).toBeInTheDocument();
  expect(result).toHaveTextContent("With 100 tries and changing is true , your chanses of winning is 50%");
});
