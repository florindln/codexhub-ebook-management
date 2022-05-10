import axios from "axios";
import FileSaver from "file-saver";
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

axios.interceptors.response.use(
  function (response) {
    // Do something with response data
    return response;
  },
  function (error) {
    if (error.response && error.response.data.message) {
      alert(error + "\n" + error.response.data.message);
    } else {
      alert(error);
    }
    // Do something with response error
    return Promise.reject(error);
  }
);

axios.interceptors.request.use(
  function (config) {
    // Do something before request is sent
    return config;
  },
  function (error) {
    alert(error);
    // Do something with request error
    return Promise.reject(error);
  }
);

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

export const DownloadBookById = async (id) => {
  return axios.get(process.env.REACT_APP_GATEWAY + "/books/download/" + id, {
    responseType: "blob",
  });
};

export const GetRatingByBookId = (bookId) => {
  return axios.get(process.env.REACT_APP_GATEWAY + "/rating/" + bookId);
};

export const GetRatingByUserAndBookId = (bookId, userId) => {
  return axios.get(
    process.env.REACT_APP_GATEWAY + "/rating/" + bookId + "/user/" + userId
  );
};

export const RateBook = (data) => {
  return axios.post(process.env.REACT_APP_GATEWAY + "/rating", data);
};

export const DeleteBook = (id) => {
  return axios.delete(process.env.REACT_APP_GATEWAY + "/books/" + id);
};

export const EditBook = (id, data) => {
  return axios.put(process.env.REACT_APP_GATEWAY + "/books/" + id, data);
};

export const CreateBook = (data) => {
  return axios.post(process.env.REACT_APP_GATEWAY + "/books", data);
};

export const GetRecommendationsByUserId = (userId, amount) => {
  return axios.get(
    process.env.REACT_APP_GATEWAY +
      "/recommendations/" +
      userId +
      "?amount=" +
      amount
  );
};

export const GetRandomRecommendations = (amount) => {
  return axios.get(
    process.env.REACT_APP_GATEWAY + "/recommendations?amount=" + amount
  );
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

export const DeleteUser = (id) => {
  return axios.delete(process.env.REACT_APP_GATEWAY + "/users/" + id);
};

//Comments
export const getComments = async () => {
  return [
    {
      id: "1",
      body: "First comment",
      username: "Jack",
      userId: "1",
      parentId: null,
      createdAt: "2021-08-16T23:00:33.010+02:00",
    },
    {
      id: "2",
      body: "Second comment",
      username: "John",
      userId: "2",
      parentId: null,
      createdAt: "2021-08-16T23:00:33.010+02:00",
    },
    {
      id: "3",
      body: "First comment first child",
      username: "John",
      userId: "2",
      parentId: "1",
      createdAt: "2021-08-16T23:00:33.010+02:00",
    },
    {
      id: "4",
      body: "Second comment second child",
      username: "John",
      userId: "2",
      parentId: "2",
      createdAt: "2021-08-16T23:00:33.010+02:00",
    },
  ];
};

export const createComment = async (text, parentId = null) => {
  return {
    id: Math.random().toString(36).substr(2, 9),
    body: text,
    parentId,
    userId: "1",
    username: "John",
    createdAt: new Date().toISOString(),
  };
};

export const updateComment = async (text) => {
  return { text };
};

export const deleteComment = async () => {
  return {};
};
