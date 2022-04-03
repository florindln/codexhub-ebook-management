import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCoffee } from "@fortawesome/free-solid-svg-icons";

function Hello() {
  return (
    <div>
      <p>hello</p>
      <FontAwesomeIcon icon="faCoffee" />
      <p>hello2</p>
    </div>
  );
}

export default Hello;
