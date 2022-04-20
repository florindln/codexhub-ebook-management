import React, { useEffect, useState } from "react";

function SvgArrow(props) {
  const [rightArrow, setRightArrow] = useState(true);

  useEffect(() => {
    setRightArrow(props.right);
  }, [props.right]);
  return (
    <div>
      {/* <div>hi {props.right}</div>{" "} */}
      {rightArrow ? (
        <svg
          className="inline icon chevron-down filter w-6 cursor-pointer"
          viewBox="0 0 20 20"
        >
          <title>Collapse filter menu</title>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            className="icon-cheveron-down"
          >
            <path
              className="secondary"
              fillRule="evenodd"
              d="M15.3 10.3a1 1 0 0 1 1.4 1.4l-4 4a1 1 0 0 1-1.4 0l-4-4a1 1 0 0 1 1.4-1.4l3.3 3.29 3.3-3.3z"
            ></path>
          </svg>
        </svg>
      ) : (
        <svg className="chevron-right filter w-6 inline" viewBox="0 0 20 20">
          <title>Expand filter menu</title>

          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            className="icon-cheveron-right"
          >
            <path
              className="secondary"
              d="M10.3 8.7a1 1 0 0 1 1.4-1.4l4 4a1 1 0 0 1 0 1.4l-4 4a1 1 0 0 1-1.4-1.4l3.29-3.3-3.3-3.3z"
            ></path>
          </svg>
        </svg>
      )}
    </div>
  );
}

export default SvgArrow;
