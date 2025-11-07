import { Box, Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import type { Student } from "../../models/Student";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

type Props = {
    student: Student;
    handleSetStudentToEdit: (id: number) => void;
    handleDelete: (id: number) => void; 
}

export default function StudentCard({student, handleSetStudentToEdit, handleDelete}: Props) {
  return (
   <Card elevation={3} sx={{ borderRadius: 3, position: 'relative' }}>      
        <CardContent>
            <Typography variant="h5">{student.name}</Typography>
            <Typography variant="body2">Age: {student.age}</Typography>
            <Typography variant="subtitle1">ID: {student.id} </Typography>  
        </CardContent>
        <CardActions sx={{display: 'flex', justifyContent: 'space-between', pb:2}}>
            <Box display='flex' gap={3}>
                <Button 
                    onClick={() => handleSetStudentToEdit(student.id!)} 
                    size="medium"
                    variant="contained">
                        <EditIcon /> View
                </Button>
                <Button 
                 size="medium" 
                 variant="contained"
                 color="error"
                 onClick={() => handleDelete(student.id!)}>
                    <DeleteIcon /> Delete
                </Button>                
            </Box>            
        </CardActions>
    </Card>
  )
}
