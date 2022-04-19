import axios from "axios";
import Cookies from "js-cookie";

const headers = {
  Accept: "application/json",
  "Content-Type": "application/json",
};

axios.interceptors.request.use(function (config) {
  const token = Cookies.get("Authorization");
  config.headers.Authorization = "Bearer " + token;

  return config;
});

//Books API

export const GetAllBooks = () => {
  //   console.log(process.env.REACT_APP_GATEWAY + "/books");
  return axios.get(process.env.REACT_APP_GATEWAY + "/books");
};

export const DeleteBook = (id) => {
  return axios.delete(process.env.REACT_APP_GATEWAY + "/books/" + id, {
    headers,
  });
};

//Auth

export const LoginRequest = (email, password) => {
  return axios.post(
    process.env.REACT_APP_GATEWAY + "/auth/login",
    { email: email, password: password },
    { headers }
  );
};
