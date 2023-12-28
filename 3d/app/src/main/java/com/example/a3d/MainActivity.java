package com.example.a3d;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;

import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.content.Intent;
import android.view.Menu;

import com.example.a3d.databinding.ActivityMainBinding;

import java.io.IOException;

public class MainActivity extends AppCompatActivity {
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        if (item.getItemId() == R.id.item2)
        {
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);

        } else if (item.getItemId() == R.id.item3)
        {
            Intent intent = new Intent(this, Favourites.class);
            startActivity(intent);

            {
                return super.onOptionsItemSelected(item);
            }

        }
        return true;
    }

    private ActivityMainBinding binding;
    private DefectAdapter adapter = new DefectAdapter();
    private int[] imageList = {
            R.drawable.defect1,
            R.drawable.defect2,
            R.drawable.defect3,
            R.drawable.defect4
    };
    private int index = 0;

    private DatabaseHelper mDBHelper;
    private SQLiteDatabase mDb;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());


        mDBHelper = new DatabaseHelper(this);

        try {
            mDBHelper.updateDataBase();
        } catch (IOException mIOException) {
            throw new Error("UnableToUpdateDatabase");
        }

        try {
            mDb = mDBHelper.getWritableDatabase();
        } catch (SQLException mSQLException) {
            throw mSQLException;
        }
        init();
    }

    private void init() {
        binding.rcView.setLayoutManager(new GridLayoutManager(this, 3));
        binding.rcView.setAdapter(adapter);

        binding.buttonAdd.setOnClickListener(v -> {
            if (index > 3) index = 0;
            Defect defect = new Defect(imageList[index], "Defect " + index, "bla bla bla");
            adapter.addDefect(defect);
            index++;
        });
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.example_menu, menu);
        return true;
    }

}