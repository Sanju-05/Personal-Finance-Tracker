let allCharts = {}; // Store charts to destroy them before re-rendering

// Function to update a pie chart with income and expense data
function updatePieChart(chartId, incomeData, expenseData) {
    let chartData;
    if (incomeData && expenseData) {
        const incomeTotal = incomeData.reduce((sum, t) => sum + t.amount, 0);
        const expenseTotal = expenseData.reduce((sum, t) => sum + t.amount, 0);
        chartData = [incomeTotal, expenseTotal];
    } else if (incomeData) {
        const incomeTotal = incomeData.reduce((sum, t) => sum + t.amount, 0);
        chartData = [incomeTotal];
    } else if (expenseData) {
        const expenseTotal = expenseData.reduce((sum, t) => sum + t.amount, 0);
        chartData = [expenseTotal];
    }

    // Destroy the old chart if it exists
    if (allCharts[chartId]) {
        allCharts[chartId].destroy();
    }

    const ctx = document.getElementById(chartId).getContext('2d');
    allCharts[chartId] = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Income', 'Expense'],
            datasets: [{
                data: chartData,
                backgroundColor: ['green', 'red']
            }]
        }
    });
}

// Function to update the charts based on filtered data
function updateCharts(filteredData) {
    console.log(filteredData);
    const incomeData = filteredData.filter(t => t.transactionType === "Income");
    const expenseData = filteredData.filter(t => t.transactionType === "Expense");

    // Main pie chart (all income and expense)
    updatePieChart('mainPieChart', incomeData, expenseData);

    // Income pie chart
    updatePieChart('incomePieChart', incomeData);

    // Expense pie chart
    updatePieChart('expensePieChart', expenseData);
}

// Simulate fetching transaction data (replace this with actual API call or data fetching)
function fetchTransactionData() {
    return [
        { amount: 1000, category: "Salary", transactionType: "Income", date: "2022-01-01" },
        { amount: 500, category: "Salary", transactionType: "Income", date: "2022-01-02" },
        { amount: 200, category: "Groceries", transactionType: "Expense", date: "2022-01-01" },
        { amount: 150, category: "Groceries", transactionType: "Expense", date: "2022-01-02" },
        { amount: 300, category: "Rent", transactionType: "Expense", date: "2022-01-01" },
        { amount: 500, category: "Rent", transactionType: "Expense", date: "2022-01-02" }
    ];
}

// Handling filter button clicks
document.getElementById("filterIncome").addEventListener("click", function () {
    const filteredData = fetchTransactionData().filter(t => t.transactionType === "Income");
    updateCharts(filteredData);
});

document.getElementById("filterExpense").addEventListener("click", function () {
    const filteredData = fetchTransactionData().filter(t => t.transactionType === "Expense");
    updateCharts(filteredData);
});

document.getElementById("filterAll").addEventListener("click", function () {
    const allData = fetchTransactionData();
    updateCharts(allData);
});

// Initialize the chart with all data
document.addEventListener("DOMContentLoaded", function () {
    const allData = fetchTransactionData();
    updateCharts(allData);
});
