import React, { useEffect, useState } from "react";
import MUIDataTable from "mui-datatables";
import { DeleteBook, GetAllBooks } from "services/APIService";

function AdminBookTable() {
  useEffect(() => {
    GetAllBooks()
      .then((response) => {
        // console.log(response.data);
        setBooks(response.data);
      })
      .catch((err) => console.log(err));
  }, []);

  const booksDefault = [
    {
      id: "01271dde-ef4f-4a74-a511-9a08d279f772",
      title: "Beginner's Guide to Quilting",
      authors: ["Elizabeth Betts", "another", "third"],
      description: "somesthing",
      pageCount: 128,
      publishedDate: "2013-06-07T00:00:00",
      category: "Crafts & Hobbies",
      thumbnailURL:
        "http://books.google.com/books/content?id=hU8uuQEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api",
      initialPrice: 0.0,
    },
    {
      id: "59444c98-9e37-4632-befb-f0598d9f4067",
      title: "Longarm Quilting Workbook",
      authors: ["Teresa Silva"],
      description:
        "Learn to Longarm with Confidence! Go from novice to successful longarm quilter with this complete guide to modern longarm quilting. Author Teresa Silva knows exactly what it's like to stand before a new longarm machine wondering where to begin. After years of longarm quilting for some of the biggest names in the quilt community, she's sharing her expertise and giving you the skills you need to longarm with confidence. Teresa covers every detail from thread selection and loading the quilt to planning a design, sewing textured stitches, and more! In Longarm Quilting Workbook, you'll learn: • Longarm machine basics, including essential features to look for when investing in your first machine • The best tools, materials, and supplies to get the job done • 20+ quilting motifs, from basic swirls and bubbles to more complex paisleys or clamshells • How to visualize, plan, and execute multiple styles of quilts through an inspiring gallery of finished samples You'll also enjoy three pieced projects perfect to practice your longarm skills and stir your creative juices. So go ahead, grab your Longarm Quilting Workbook and work aside Teresa Silva to longarm beautiful quilts in no time!",
      pageCount: 144,
      publishedDate: "2017-10-04T00:00:00",
      category: "Crafts & Hobbies",
      thumbnailURL:
        "http://books.google.com/books/content?id=BhZjDwAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
      initialPrice: 38.0,
    },
  ];

  const [books, setBooks] = useState(booksDefault);

  const bookColumns = [
    {
      name: "id",
      label: "Id",
      options: {},
    },
    {
      name: "title",
      label: "title",
      options: {},
    },
    {
      name: "authors",
      label: "authors",
      options: {
        customBodyRender: (value, tableMeta, updateValue) => (
          <div>{value.join(",")}</div>
        ),
      },
    },
    {
      name: "pageCount",
      label: "pageCount",
      options: {},
    },
    {
      name: "publishedDate",
      label: "publishedDate",
      options: {},
    },
    {
      name: "category",
      label: "category",
      options: {},
    },
    {
      name: "thumbnailURL",
      label: "thumbnailURL",
      options: {},
    },
  ];

  const options = {
    filterType: "checkbox",
    selectableRows: "single",
    onRowsDelete: (rowsDeleted, rowData) => {
      const bookId = rowData[0][0];
      //   console.log(bookId); //do axios call to delete book
      DeleteBook(bookId);
    },
  };

  return (
    <div>
      <MUIDataTable
        title={"Employee List"}
        data={books}
        columns={bookColumns}
        options={options}
      />
    </div>
  );
}

export default AdminBookTable;
