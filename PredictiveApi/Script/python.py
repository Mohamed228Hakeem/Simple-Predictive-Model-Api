import pandas as pd
from sklearn.linear_model import LinearRegression
import sys
import warnings
from sklearn.exceptions import ConvergenceWarning

# Path to your CSV file
# CSV_FILE_PATH = r'D:\College\4th Year Prep\C# Apis\Analytics Api\PredictiveApi\Controllers\sales.csv', encoding='utf8'

producttobepredicted = "Product B"

def get_data_from_csv():
    try:
        # Read the CSV file
        df = pd.read_csv(r'D:\College\4th Year Prep\C# Apis\Analytics Api\PredictiveApi\Script\DataSets\sales.csv', encoding='utf8')
        # Filter data for a specific product
        return df[df['ProductName'] == producttobepredicted]
    except FileNotFoundError:
        print(f"Error: The file  was not found.")
        sys.exit(1)
    except Exception as e:
        print(f"Error reading CSV file: {e}")
        sys.exit(1)

def train_model(df):
    try:
        df['Date'] = pd.to_datetime(df['Date'])  # Convert the date to datetime
        df['DayOfYear'] = df['Date'].dt.dayofyear  # Extract the day of the year
        Xion = df[['DayOfYear']]
        y = df['SalesQuantity']
        model = LinearRegression().fit(Xion, y)  # Train the linear regression model
        return model
    except Exception as e:
        print(f"Error training the model: {e}")
        sys.exit(1)

def predict_demand(model, day_of_year):
    try:
        prediction = model.predict([[day_of_year]])
        return prediction[0]
    except Exception as e:
        print(f"Error predicting demand: {e}")
        sys.exit(1)

if __name__ == "__main__":
    try:
        # Get data from CSV
        df = get_data_from_csv()
        # Train the model
        model = train_model(df)
        # Check if arguments are provided
        if len(sys.argv) > 1:
            day_of_year = int(sys.argv[1])
            # Make prediction
            prediction = predict_demand(model, day_of_year)
            print(round(prediction),"units of ", producttobepredicted , " will be sold on the specified day : ",day_of_year," .")
        else:
            print("Please provide the day of the year as an argument.")
            sys.exit(1)
    except Exception as e:
        print(f"Unexpected error: {e}")
        sys.exit(1)
