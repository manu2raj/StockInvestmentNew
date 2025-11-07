import React, { useEffect, useState } from 'react';
import { getStudents, deleteStudent } from '../services/studentservices';
import type { Student } from '../models/Student';
import StudentForm from './StudentForm';
import { List, ListItem, ListItemText, IconButton, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

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

  return (
    <>
      <StudentForm studentToEdit={studentToEdit} onSave={loadStudents} />
      <Typography variant="h5" sx={{ mt: 4 }}>Student List</Typography>
      <List>
        {students.map(s => (
          <ListItem key={s.id} secondaryAction={
            <>
              <IconButton edge="end" onClick={() => setStudentToEdit(s)}>
                <EditIcon />
              </IconButton>
              <IconButton edge="end" onClick={() => handleDelete(s.id!)}>
                <DeleteIcon />
              </IconButton>
            </>
          }>
            <ListItemText primary={`${s.name} (${s.age})`} />
          </ListItem>
        ))}
      </List>
    </>
  );
};

export default StudentList;