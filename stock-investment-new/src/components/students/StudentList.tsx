import React, { useEffect, useState } from 'react';
import { getStudents, deleteStudent } from '../../services/studentservices';
import type { Student } from '../../models/Student';
import StudentForm from './StudentForm';
import {  Typography, Box } from '@mui/material';
import StudentCard from './StudentCard';


const StudentList: React.FC = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [studentToEdit, setStudentToEdit] = useState<Student | undefined>(undefined);

  const loadStudents = () => {
    getStudents().then(res => setStudents(res.data));
  };

  useEffect(() => {
    loadStudents();
  }, []);

  const handleDelete = (id: number) => {
    deleteStudent(id).then(loadStudents);
  };

  const handleSetStudentToEdit = (id: number) => {
    setStudentToEdit(students!.find(a => a.id === id));
  }

  return (
    <>
      <StudentForm studentToEdit={studentToEdit} onSave={loadStudents} />
      <Typography variant="h5" sx={{ mt: 4 }}>Student List</Typography>
      <Box  className="flex-container" sx={{display: 'flex', flexDirection: 'row', gap: 1}}> 
        {students.map(student => (
            <StudentCard 
              key={student.id}
              student={student} 
              handleSetStudentToEdit={handleSetStudentToEdit}
              handleDelete={handleDelete} 
            />
          ))
        }
      </Box>
    </>
  );
};

export default StudentList;