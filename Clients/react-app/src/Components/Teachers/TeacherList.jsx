import { useEffect, useState } from 'react';

import TeacherItem from './TeacherItem';

function TeacherList() {
  const [teachers, setTeachers] = useState([]);

 
  useEffect(() => {
    loadTeachers();
  }, []);

  const loadTeachers = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    console.log(token);

    const url = `${process.env.REACT_APP_BASEURL}/teachers/listteachers`;
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log('Something went wrong!');
    } else {
      setTeachers(await response.json());
    }
  };

  const deleteTeachers = async (id) => {
    console.log(`Remove the teacher with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/teachers/${id}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });

    console.log(response);

    if (response.status >= 200 && response.status <= 299) {
      console.log('teacher removed');
      loadTeachers();
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
          <th>Expertness</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {teachers.map((teacher) => (
          <TeacherItem
            teacher={teacher}
            key={teacher.teacherId}
            handleDeleteTeacher={deleteTeachers}
          />
        ))}
      </tbody>
    </table>
  );
}

export default TeacherList;
