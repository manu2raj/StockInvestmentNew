import React, { useState, useEffect } from 'react';
import { Box, Card, Typography } from '@mui/material'; // Example using Material UI

interface StockData {
    id: number;
  date: string;
  price: number;
}


const StockDashboard: React.FC = () => {
  const [stockPrices, setStockPrices] = useState<StockData[]>([]);

  useEffect(() => {
    // Simulate fetching stock data
    const fetchData = async () => {
      // In a real application, this would be an API call
      const data: StockData[] = [
        {id:1, date: '2025-10-01', price: 150 },
        {id:2,  date: '2025-10-02', price: 152 },
        {id:3,  date: '2025-10-03', price: 148 },
        {id:4,  date: '2025-10-04', price: 155 },
        {id:5,  date: '2025-10-05', price: 153 },
      ];
      setStockPrices(data);
    };
    fetchData();
  }, []);

  return (
      <Box sx={{display: 'flex', flexDirection: 'column', gap: 3}}> 
            {stockPrices.map(stock => (
                <Card elevation={3} sx={{ borderRadius: 3, position: 'relative' }}>
                    <Typography key={stock.id}>
                        {stock.date}: ${stock.price}
                    </Typography>
                </Card>
            ))};  
        </Box>
  );
};

export default StockDashboard;