import React, { useEffect, useState } from "react";
import { MultiSelect } from "react-multi-select-component";

const defaultOptions = [
  { label: "Grapes 🍇", value: "grapes" },
  { label: "Mango 🥭", value: "mango" },
  { label: "Strawberry 🍓", value: "strawberry", disabled: true },
];

const Multiselecter = (props) => {
  const [selected, setSelected] = useState([]);
  const [options, setOptions] = useState(defaultOptions);

  useEffect(() => {
    //get all book genres here then put them in options
    let categories = props.categories;
    let formattedCategories = [];
    categories.forEach((category) => {
      formattedCategories.push({
        label: category,
        value: category,
      });
    });
    setOptions(formattedCategories);
  }, []);

  useEffect(() => {
    props.onHandleSelectedGenres(selected);
  }, [selected]);

  return (
    <div>
      <h5>Select Genres</h5>
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
