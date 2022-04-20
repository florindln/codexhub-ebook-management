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
  return axios.get(process.env.REACT_APP_GATEWAY + "/books");
};

export const FilterBooks = (params) => {
  return axios.get(process.env.REACT_APP_GATEWAY + "/books/filter", {
    params: params,
  });
};

export const GetBookById = (id) => {
  return axios.get(process.env.REACT_APP_GATEWAY + "/books/" + id);
};

export const DeleteBook = (id) => {
  return axios.delete(process.env.REACT_APP_GATEWAY + "/books/" + id);
};

export const EditBook = (id, data) => {
  return axios.put(process.env.REACT_APP_GATEWAY + "/books/" + id, data);
};

//Auth

export const LoginRequest = (email, password) => {
  return axios.post(process.env.REACT_APP_GATEWAY + "/auth/login", {
    email: email,
    password: password,
  });
};

export const Register = async (data) => {
  // console.log(data);
  return axios.post(process.env.REACT_APP_GATEWAY + "/auth/register", data);
};

//Users
export const GetUserById = (id) => {
  return axios.get(process.env.REACT_APP_GATEWAY + "/users/" + id);
};

export const EditUser = (id, data) => {
  return axios.put(process.env.REACT_APP_GATEWAY + "/users/" + id, data);
};
