import { useEffect, useState } from 'react';

import StudentItem from './StudentItem';

function StudentList() {
  const [students, setStudents] = useState([]);

 
  useEffect(() => {
    loadStudents();
  }, []);

  const loadStudents = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    console.log(token);

    const url = `${process.env.REACT_APP_BASEURL}/students/liststudents`;
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log('Something went wrong!');
    } else {
      setStudents(await response.json());
    }
  };

  const deleteStudent = async (id) => {
    console.log(`Remove the student with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/students/${id}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });

    console.log(response);

    if (response.status >= 200 && response.status <= 299) {
      console.log('student removed');
      loadStudents();
    } else {
      console.log('Somthing went wrong!');
    }
  };

  return (
    <table>
      <thead>
        <tr>
          <th></th>
          <th>Firstname</th>
          <th>Lastname</th>
          <th>Email</th>
          <th>TelefonNumber</th>
          <th>Address</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {students.map((student) => (
          <StudentItem
            student={student}
            key={student.studentId}
            handleDeleteStudent={deleteStudent}
          />
        ))}
      </tbody>
    </table>
  );
}

export default StudentList;
