import React, { useEffect, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { FilterBooks, GetAllBooks } from "services/APIService";
import BookCard from "./BookCard";
import BookFilterForm from "./BookFilterForm";
import SvgArrow from "./SvgArrow";
import "./Browse.css";
import { useSearchParams } from "react-router-dom";
import InfiniteScroll from "react-infinite-scroll-component";

function Browse() {
  const defaultBooks = [
    {
      title: "title",
      authors: ["author1", "author1"],
      description: "description",
      pageCount: 300,
      initialPrice: 300,
      publishedDate: "publishedDate",
      thumbnailUrl: "https://picsum.photos/200",
      category: "category",
    },
    {
      title: "title2",
      authors: ["author1", "author2"],
      description: "description2",
      pageCount: 300,
      publishedDate: "publishedDate",
      thumbnailUrl: "https://picsum.photos/200",
      category: "category2",
    },
  ];

  const [dropdownToggled, setDropdownToggled] = useState(false);
  const [books, setBooks] = useState(defaultBooks);
  const [viewableBooks, setViewableBooks] = useState([]);
  const [searchParams, setSearchParams] = useSearchParams();
  const nameSearch = searchParams.get("name");

  const handleFilter = () => {
    setDropdownToggled(!dropdownToggled);
  };

  useEffect(() => {
    if (nameSearch === null) {
      GetAllBooks().then((response) => {
        setBooks(response.data);
        setViewableBooks(response.data.slice(0, 5));
      });
    } else {
      FilterBooks({ title: nameSearch }).then((response) => {
        setBooks(response.data);
        setViewableBooks(response.data.slice(0, 5));
      });
      // console.log(nameSearch);
    }
  }, [nameSearch]);

  const handleFilterBooks = (books) => {
    setBooks(books);
  };

  const extractCategoriesFromBooks = () => {
    let genreList = [];
    books.forEach((book) => {
      if (!genreList.includes(book.category)) {
        genreList.push(book.category);
      }
    });

    return genreList;
  };

  const fetchMoreData = () => {
    let length = viewableBooks.length;
    let newBooks = books.slice(length, length + 5);
    let newViewable = [...viewableBooks, ...newBooks];
    setViewableBooks(newViewable);
  };

  return (
    <div>
      <Container fluid>
        <Row>
          <Col md={2}></Col>
          <Col md={8} className="d-flex justify-content-start">
            <Container>
              <Row className="py-3">
                <h2>Browse all books</h2>
              </Row>
              <Row className="border border-dark rounded py-3 nicehover">
                <Col onClick={handleFilter} xs={1}>
                  <div className="" style={{ width: "5rem" }}>
                    <SvgArrow right={dropdownToggled} />
                  </div>
                </Col>
                <Col
                  onClick={handleFilter}
                  xs={11}
                  className="d-flex justify-content-start align-items-center "
                >
                  <h2 className=" ps-3"> Filter all books</h2>
                </Col>
                {dropdownToggled && (
                  <BookFilterForm
                    categories={extractCategoriesFromBooks()}
                    onHandleFilterBooks={handleFilterBooks}
                  />
                )}
              </Row>
              <Row>
                <InfiniteScroll
                  dataLength={viewableBooks.length}
                  next={fetchMoreData}
                  hasMore={true}
                  loader={<></>}
                >
                  {viewableBooks.map((book, index) => (
                    <BookCard key={index} book={book} />
                  ))}
                  {/* {books.map((book, index) => (
                    <BookCard key={index} book={book} />
                  ))} */}
                </InfiniteScroll>
              </Row>
            </Container>
          </Col>
          <Col md={2}></Col>
        </Row>
      </Container>
    </div>
  );
}

export default Browse;
