package com.example.a3d;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;

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

        binding.rcView.setLayoutManager(new LinearLayoutManager(this));
        binding.rcView.setAdapter(adapter);
        adapter.addDefect(new Defect(imageList[0], "Коробление\n(отклеивание от платформы)", "Распечатка не прилипает к платформе 3D-принтера, отклеивается от платформы, изгибается. Из-за перехода пластика из одного состояния  в другое и изменения температур, пластик начинает уменьшаться в объеме. Этот процесс проходит неравномерно - сначала остывают края, а затем только центральная часть. Из-за этого возникают внутренние напряжения, которые отрывают края или ломают деталь."));
        adapter.addDefect(new Defect(imageList[1], "Недостаточное экструдирование\n(Недоэкструзия)", "Дырки в печати, щели между соседними слоями. Поверхность детали выходит не ровной, а со всякими вкраплениями, либо отсутствием пластика там, где он необходим."));
        adapter.addDefect(new Defect(imageList[2], "Избыточное экструдирование", "Принтер поставляет больше материала, чем необходимо.  На распечатке есть излишки филамента."));
        adapter.addDefect(new Defect(imageList[3], "Щели или дыры на верхнем слое", "Щели или дыры на верхнем слое изделия."));
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.example_menu, menu);
        return true;
    }

}