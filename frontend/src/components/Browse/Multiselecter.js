import React, { useEffect, useState } from "react";
import { MultiSelect } from "react-multi-select-component";

const options = [
  { label: "Grapes 🍇", value: "grapes" },
  { label: "Mango 🥭", value: "mango" },
  { label: "Strawberry 🍓", value: "strawberry", disabled: true },
];

const Multiselecter = (props) => {
  const [selected, setSelected] = useState([]);

  useEffect(() => {
    //get all book genres here then put them in options
  }, []);

  useEffect(() => {
    props.onHandleSelectedGenres(selected);
  }, [selected]);

  return (
    <div>
      <h4>Select Genres</h4>
      {/* <pre>{JSON.stringify(selected)}</pre> */}
      <MultiSelect
        options={options}
        value={selected}
        onChange={setSelected}
        labelledBy="Select"
      />
    </div>
  );
};

export default Multiselecter;
