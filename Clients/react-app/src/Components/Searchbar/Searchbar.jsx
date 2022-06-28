import React from 'react';
import CourseList from '../Course/CourseList';
import CourseItem from '../Course/CourseItem';

export default function SearchCourseByCategory ({ course }) {
  const category = course[0] && Object.keys(course[0]);
  return (
    <table cellPadding={0} cellSpacing={0}>
      <thead>
        <tr>
          {course[0] && category.map((heading) => <th>{heading}</th>)}
        </tr>
      </thead>
      <tbody>
        {course.map((course) => (
          <tr>
            {category.map((category) => (
              <td>{course[category]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}